using BombermanGame.Shared.Interfaces;

namespace Tournament
{
    public static class TypeExtension
    {
        public static IPlayer CreatePlayer(this Type type)
        {
            if (!type.IsInterfaceImplemented(typeof(IPlayer)))
            {
                throw new ArgumentException($"Wrong argument, type [{type.FullName}] " +
                                            $"is not implement interface [{typeof(IPlayer).FullName}]");
            }

            var constructor = type.GetConstructor(new Type[0]);

            if (constructor == null)
            {
                throw new ArgumentException($"Wrong argument, type [{type.FullName}] " +
                                            $"is not contained constructor without arguments");
            }
            
            return  (IPlayer)constructor.Invoke(null);
        }

        public static bool IsInterfaceImplemented(this Type type, Type interfaceType)
        {
            return type.FindInterfaces(
                (type, _) => type.FullName == interfaceType.FullName,
                null).Any();
        }

    }
}