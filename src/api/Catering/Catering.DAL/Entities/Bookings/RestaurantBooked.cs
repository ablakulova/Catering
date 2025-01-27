﻿namespace Catering.DAL.Entities.Bookings
{
    public class RestaurantBooked
    {
        public RestaurantBooked()
        {

        }

        public RestaurantBooked(int restaurantBookedId, string restaurantName, string pictureUrl)
        {
            RestaurantBookedId = restaurantBookedId;
            RestaurantName = restaurantName;
            PictureUrl = pictureUrl;
        }

        public int RestaurantBookedId { get; set; }

        public string RestaurantName { get; set; }

        public string PictureUrl { get; set; }
    }
}