using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GameDefinations;

public static partial class ModHandler
{
    public static string DLLFolder = @"\Data\DLL";
    
    public static void Initialize()
    {
        List<Assembly> assemblies = new List<Assembly>();

        string fullPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        fullPath += DLLFolder;

        string[] files = Directory.GetFiles(fullPath);

        assemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies());

        foreach (string file in files)
        {
            if(Path.GetExtension(file).ToLower() == ".dll")
            {
                Assembly currAssembly = Assembly.LoadFile(file);
                assemblies.Add(currAssembly);
            }
        }

        InitializeAssembly(assemblies.ToArray());
    }

    public static void LoadPackets()
    {
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var types = assemblies
            .SelectMany(s => s.GetTypes())
            .Where(t => t.IsSubclassOf(typeof(Packet)));

        foreach (var type in types)
        {
            Packet packet = (Packet)Activator.CreateInstance(type);
            MainServer.packets.Add(packet.type, packet);
        }
    }

    public static bool ImplementsInterface(this Type type, Type interfaceType)
    {
        return type.GetInterfaces().Contains(interfaceType);
    }

    public static void InitializeAssembly(Assembly[] assembilies)
    {
        List<Initializable> inits = new List<Initializable>();

        for(int i = 0; i < assembilies.Length; i++)
        {
            Assembly currAssembly = assembilies[i];
            Type[] types = currAssembly.GetTypes();
            
            foreach (Type type in currAssembly.GetTypes())
            {
                if (!type.ImplementsInterface(typeof(Initializable))) { continue; }
                
                Initializable toInit = (Initializable)Activator.CreateInstance(type);
                inits.Add(toInit);
            }
        }

        inits.Sort((x, y) => x.priority.CompareTo(y.priority));
        for (int i = 0; i < inits.Count; i++)
        {
            inits[i].Initialize();
        }
    }
}
