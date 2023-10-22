using Games.BombermanGame.Shared.Enums;

namespace Games.BombermanGame.Shared.Delegates;

public delegate T? FieldEnumeratorDelegate<T>(int rowIndex, int columnIndex, FieldItemEnum cell) where T:struct;
