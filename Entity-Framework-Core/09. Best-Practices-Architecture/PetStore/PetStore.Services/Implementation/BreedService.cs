﻿namespace PetStore.Services.Implementation
{
    using System;
    using System.Linq;
    using PetStore.Data;
    using PetStore.Models;

    public class BreedService : IBreedService
    {
        private readonly PetStoreDbContext data;

        public BreedService(PetStoreDbContext data)
        {
            this.data = data;
        }

        public void Add(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Breed name cannot be null or whitespace.");
            }

            var breed = new Breed()
            {
                Name = name
            };

            this.data.Add(breed);
            this.data.SaveChanges();
        }

        public bool Exists(int breedId)
        {
            return this.data.Breed.Any(b => b.Id == breedId);
        }
    }
}
