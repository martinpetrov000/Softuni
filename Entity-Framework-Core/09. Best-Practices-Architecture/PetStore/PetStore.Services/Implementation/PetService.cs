﻿namespace PetStore.Services.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using PetStore.Data;
    using PetStore.Models;
    using PetStoreServices.Models.Pet;

    public class PetService : IPetService
    {
        private readonly PetStoreDbContext data;
        private readonly IBreedService breedService;
        private readonly ICategoryService categoryService;
        private readonly IUserService userService;

        public PetService(PetStoreDbContext data, IBreedService breedService, ICategoryService categoryService, IUserService userService)
        {
            this.data = data;
            this.breedService = breedService;
            this.categoryService = categoryService;
            this.userService = userService;
        }

        public void BuyPet(Gender gender, DateTime dateOfBirth, decimal price, string description, int breedId, int categoryId)
        {
            if (price < 0)
            {
                throw new ArgumentException("Price of the pet cannot be less than zero.");
            }

            if (!this.breedService.Exists(breedId))
            {
                throw new ArgumentException("There is no such breed with given id in the database.");
            }

            if (!this.categoryService.Exists(categoryId))
            {
                throw new ArgumentException("There is no such category with given id in the database.");
            }

            var pet = new Pet()
            {
                Gender = gender,
                DateOfBirth = dateOfBirth,
                Price = price,
                Description = description,
                BreedId = breedId,
                CategoryId = categoryId
            };

            this.data.Pets.Add(pet);
            this.data.SaveChanges();
        }

        public void SellPet(int petId, int userId)
        {
            if (!this.userService.Exists(userId))
            {
                throw new ArgumentException("There is no such user with given id in the database.");
            }

            if (!this.Exists(petId))
            {
                throw new ArgumentException("There is no such pet with given id in the database.");
            }

            var pet = this.data.Pets
                .First(p => p.Id == petId);

            var order = new Order()
            {
                PurchaseDate = DateTime.Now,
                Status = OrderStatus.Done,
                UserId = userId
            };

            this.data.Orders.Add(order);
            pet.Order = order;
            this.data.SaveChanges();
        }

        public bool Exists(int petId)
        {
            return this.data.Pets.Any(b => b.Id == petId);
        }

        public IEnumerable<PetListingServiceModel> All()
            => this.data
                .Pets
                .Select(p => new PetListingServiceModel
                {
                    Id = p.Id,       
                    Price = p.Price,
                    Breed = p.Breed.Name,
                    Category = p.Category.Name
                })
            .ToList();
    }
}
