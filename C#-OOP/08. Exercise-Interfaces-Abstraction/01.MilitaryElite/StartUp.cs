﻿using MilitaryElite.Core;

namespace MilitaryElite
{
    class StartUp
    {
        static void Main(string[] args)
        {
            ICommandInterpreter command = new CommandInterpreter();
            IEngine engine = new Engine(command);
            engine.Run();
        }
    }
}
