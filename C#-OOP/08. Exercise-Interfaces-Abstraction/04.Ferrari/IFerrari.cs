﻿namespace Ferrari
{
    public interface IFerrari
    {
        string Model { get; }

        string Driver { get; }

        string Brakes();

        string Gas();
    }
}
