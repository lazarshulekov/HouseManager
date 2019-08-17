using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HouseManager.Controllers
{
    using BLL;

    using DAL.Models;

    public class PropertyTypesController : Controller
    {
        private readonly IPropertyTypeService propertyTypeService;

        public PropertyTypesController(IPropertyTypeService propertyTypeService)
        {
            this.propertyTypeService = propertyTypeService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await propertyTypeService.GetAllPropertyTypesAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var propertyType = await propertyTypeService.GetPropertyTypeByIdAsync(id);
            if (propertyType == null)
            {
                return NotFound();
            }

            return View(propertyType);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type")] PropertyType propertyType)
        {
            if (ModelState.IsValid)
            {
                await propertyTypeService.AddAsync(propertyType);
                return RedirectToAction("Index");
            }
            return View(propertyType);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var propertyType = await propertyTypeService.GetPropertyTypeByIdAsync(id);
            if (propertyType == null)
            {
                return NotFound();
            }
            return View(propertyType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type")] PropertyType propertyType)
        {
            if (id != propertyType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await propertyTypeService.UpdateAsync(propertyType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyTypeExists(propertyType.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(propertyType);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var propertyType = await propertyTypeService.GetPropertyTypeByIdAsync(id);
            if (propertyType == null)
            {
                return NotFound();
            }

            return View(propertyType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var propertyType = await propertyTypeService.GetPropertyTypeByIdAsync(id);
           await propertyTypeService.DeleteAsync(propertyType);
            return RedirectToAction("Index");
        }

        private bool PropertyTypeExists(int id)
        {
            return (propertyTypeService.GetPropertyTypeByIdAsync(id)!= null);
        }
    }
}
