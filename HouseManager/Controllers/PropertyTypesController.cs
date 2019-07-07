using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Persistence.Models;

namespace HouseManager.Controllers
{
    using BLL;

    public class PropertyTypesController : Controller
    {
        private readonly IPropertyTypeService propertyTypeService;

        public PropertyTypesController(IPropertyTypeService propertyTypeService)
        {
            propertyTypeService = propertyTypeService;
        }

        // GET: PropertyTypes
        public async Task<IActionResult> Index()
        {
            return View(await propertyTypeService.GetAllPropertyTypesAsync());
        }

        // GET: PropertyTypes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var propertyType = await propertyTypeService.GetPropertyTypeByIdAsync(id);
            if (propertyType == null)
            {
                return NotFound();
            }

            return View(propertyType);
        }

        // GET: PropertyTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PropertyTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: PropertyTypes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyType = await propertyTypeService.GetPropertyTypeByIdAsync(id);
            if (propertyType == null)
            {
                return NotFound();
            }
            return View(propertyType);
        }

        // POST: PropertyTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: PropertyTypes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyType = await propertyTypeService.GetPropertyTypeByIdAsync(id);
            if (propertyType == null)
            {
                return NotFound();
            }

            return View(propertyType);
        }

        // POST: PropertyTypes/Delete/5
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
