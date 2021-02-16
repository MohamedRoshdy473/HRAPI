using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HrAPI.DTO;
using HrAPI.Models;
using HrAPI.ViewModels;

namespace HrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SubCategories
        [HttpGet]
        public IEnumerable<SubCategoriesDTO> GetSubCategoriesDTO()
        {
            var subcategory = _context.SubCategories.Select(s => new SubCategoriesDTO
            {
                ID = s.ID,
                SubCategoryName = s.SubCategoryName,
                CategoryName = s.needsCategory.Name
            }).ToList();
            return subcategory;
        }

        // GET: api/SubCategories/5
        [HttpGet("{id}")]
        public SubCategoriesDTO GetSubCategoriesDTO(int id)
        {
            //var subCategory = _context.SubCategories.Where(a => a.ID == id).ToList();
            var subCategory = _context.SubCategories.Include(s => s.needsCategory).FirstOrDefault(s=>s.ID==id);
            var subCategoriesDTO = new SubCategoriesDTO
            {
                ID = subCategory.ID,
                SubCategoryName = subCategory.SubCategoryName,
                CategoryName = subCategory.needsCategory.Name,
               CategoryID=subCategory.CategoryID
            };
            return subCategoriesDTO;
        }


     //   [HttpGet("{categoryId}")]
        [Route("GetSubCategoriesByCategoryId/{categoryId}")]
        public List<SubCategoriesDTO> GetSubCategoriesByCategoryId(int categoryId)
        {
            var needs= _context.SubCategories.Where(a => a.CategoryID == categoryId).Select(item =>
          new SubCategoriesDTO
          {
              ID = item.ID,
              SubCategoryName = item.SubCategoryName
          }).ToList();
            return needs;
        }

        // PUT: api/SubCategories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<SubCategory>> PutSubCategoriesDTO(int id, SubCategoryViewModel subCategoryViewModel)
        {
            if (id != subCategoryViewModel.ID)
            {
                return BadRequest();
            }
            var subCategories = new SubCategory
            {
                ID = subCategoryViewModel.ID,
                SubCategoryName = subCategoryViewModel.SubCategoryName,
                CategoryID = subCategoryViewModel.CategoryID
            };
            _context.Entry(subCategories).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubCategoriesDTOExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SubCategories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SubCategoriesDTO>> PostSubCategoriesDTO(SubCategoryViewModel subCategoryViewModel)
        {
            var subCategories = new SubCategory
            {
                ID = subCategoryViewModel.ID,
                SubCategoryName = subCategoryViewModel.SubCategoryName,
                CategoryID = subCategoryViewModel.CategoryID
            };
            _context.SubCategories.Add(subCategories);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubCategoriesDTO", new { id = subCategoryViewModel.ID }, subCategoryViewModel);
        }

        // DELETE: api/SubCategories/5
        [HttpDelete("{id}")]
       // [Route("api/SubCategories/{id}")]
        public async Task<ActionResult<SubCategory>> DeleteSubCategoriesDTO(int id)
        {
            var subCategories = await _context.SubCategories.FindAsync(id);
            if (subCategories == null)
            {
                return NotFound();
            }

            _context.SubCategories.Remove(subCategories);
            await _context.SaveChangesAsync();

            return subCategories;
        }

        private bool SubCategoriesDTOExists(int id)
        {
            return _context.SubCategories.Any(e => e.ID == id);
        }
    }
}
