using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlacesWebApp.Data;
using PlacesWebApp.Models;
using PlacesWebApp.Models.ViewModels;

namespace PlacesWebApp.Controllers
{
    public class PlaceController : Controller
    {
        private readonly PlacesDbContext placesDbContext;

 
        public PlaceController(PlacesDbContext placesDbContext) 
        {
            this.placesDbContext = placesDbContext;
        }
        public IActionResult Add()
        {
          

            return View();
        }

        [HttpPost]
     
        public IActionResult Add(AddPlaceRequest addPlaceRequest)
        {
          
                //Request to PlaceItem Model
                var placeItem = new PlaceItem
                {
                    PlaceName = addPlaceRequest.placeName
                };

                //Check if already exist
                var existingPlace = placesDbContext.PlacesTable.FirstOrDefault(p => p.PlaceName == placeItem.PlaceName);
                if (existingPlace != null)
                {
                    TempData["ErrorMessage"] = "Place with this name already exists. Please choose a different name.";
                    return RedirectToAction("Add");

                }


                placesDbContext.PlacesTable.Add(placeItem);
                placesDbContext.SaveChanges();

                return RedirectToAction("Index", "Home");
                //return View();



        }
        public IActionResult Edit(int id)
        {
            var place = placesDbContext.PlacesTable.FirstOrDefault(p => p.PlaceId == id);

            if (place == null)
            {
                return NotFound();
            }

            return View(place);
        }

        [HttpPost]
        public IActionResult Edit(PlaceItem placeItem)
        {
            if (ModelState.IsValid)
            {
                var existingPlace = placesDbContext.PlacesTable.FirstOrDefault(p => p.PlaceId == placeItem.PlaceId);

                if (existingPlace == null)
                {

                    return NotFound();
                }

                existingPlace.PlaceName = placeItem.PlaceName;

                // Update & Save
                placesDbContext.Update(existingPlace);
                placesDbContext.SaveChanges();

                // Redirect to the home page
                return RedirectToAction("Index", "Home");
            }

            return View(placeItem);
        }


   

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var place = placesDbContext.PlacesTable.FirstOrDefault(p => p.PlaceId == id);

            if (place == null)
            {
                return NotFound();
            }
            // Remove & Save
            placesDbContext.PlacesTable.Remove(place);
            placesDbContext.SaveChanges();

            // Redirect to the home page
            return RedirectToAction("Index", "Home");
        }


    }
}
