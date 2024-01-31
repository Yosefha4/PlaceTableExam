using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PlacesWebApp.Models
{
    public class PlaceItem
    {

        [Key] public int PlaceId { get; set; }

        [Remote(action: "IsPlaceNameUnique", controller: "Place", ErrorMessage = "PlaceName must be unique.")] public required string PlaceName { get; set; }
    }
}
