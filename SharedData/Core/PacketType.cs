public enum PacketType
{
    Unknown,

    LoginRequest,
    CharacterCreationRequest,
    PlayerSyncRequest,
    PlayerMovementRequest,

    LoginAnswer,
    CharacterCreationAnswer,
    PlayerSyncAnswer,
    PlayerMovementUpdate,

    PlayerDisconnected,
}