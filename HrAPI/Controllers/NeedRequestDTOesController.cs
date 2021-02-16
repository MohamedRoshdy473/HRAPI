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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HrAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class NeedRequestDTOesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private Employee CurrentEmp;


        public NeedRequestDTOesController(ApplicationDbContext context)
        {
            _context = context;
        }
        private Employee CurrentEmployee()
        {
            var CurrentEmp = new Employee();
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var lstUsers= _context.Employees.Where(e => e.Email == email).ToList();
            if (lstUsers.Count>0)
            {
                CurrentEmp = lstUsers[0];
            }
            return CurrentEmp;
        }
        [Route("GetNeedRequestforEmployee/{id}")]
        public IEnumerable<NeedRequestDTO> GetNeedRequestDTOforEmployee(int id)
        {
           // id = (int)CurrentEmployee().ID;
            var needrequest = _context.NeedsRequests
                .Where(n=>n.EmployeeID==id)
                .Select(n => new NeedRequestDTO
            {
                ID = n.ID,
                EmployeeName = n.Employee.Name,
                EmployeeId=n.EmployeeID,
                CategoryName = n.needsCategory.Name,
                SubCategoryName = n.subCategory.SubCategoryName,
                NeedRequestDate = n.NeedRequestDate,
                Status=n.Status,
                Comment = n.Comment
            }).ToList();
            return needrequest;
        }

        [Route("GetNeedRequestforEmployeeByCategory/{id}/{categoryId}")]
        public IEnumerable<NeedRequestDTO> GetNeedRequestDTOforEmployeeByCategory(int id,int categoryId)
        {
            // id = (int)CurrentEmployee().ID;
            var needrequest = _context.NeedsRequests
                .Where(n => n.EmployeeID == id && n.CategoryID== categoryId)
                .Select(n => new NeedRequestDTO
                {
                    ID = n.ID,
                    EmployeeName = n.Employee.Name,
                    EmployeeId = n.EmployeeID,
                    CategoryName = n.needsCategory.Name,
                    SubCategoryName = n.subCategory.SubCategoryName,
                    NeedRequestDate = n.NeedRequestDate,
                    Status = n.Status,
                    Comment = n.Comment
                }).ToList();
            return needrequest;
        }

        // GET: api/NeedRequestDTOes
        [HttpGet]
        public IEnumerable<NeedRequestDTO> GetNeedRequestDTO()
        {
            var needrequest = _context.NeedsRequests.Select(n => new NeedRequestDTO
            {
                ID = n.ID,
                EmployeeName =n.Employee.Name,
                CategoryName = n.needsCategory.Name,
                SubCategoryName = n.subCategory.SubCategoryName,
                NeedRequestDate = n.NeedRequestDate,
                Status = n.Status,
                Comment = n.Comment
            }).ToList();
           // return needrequest;
            return needrequest;
        }
        [Route("GetNeedRequestByManager")]
        public IEnumerable<NeedRequestDTO> GetNeedRequestByManager()
        {
            var needrequest = _context.NeedsRequests.Where(ex => ex.Employee.Profession.ManagerID == CurrentEmployee().ID).Select(n => new NeedRequestDTO
            {
                ID = n.ID,
                EmployeeName = n.Employee.Name,
                CategoryName = n.needsCategory.Name,
                SubCategoryName = n.subCategory.SubCategoryName,
                NeedRequestDate = n.NeedRequestDate,
                Status = n.Status,
                Comment = n.Comment
            }).ToList();
            // return needrequest;
            return needrequest;
        }
        [Route("GetPendingNeedRequestByManager")]
        public IEnumerable<NeedRequestDTO> GetPendingNeedRequestByManager()
        {
            var needrequest = _context.NeedsRequests.Where(ex => ex.Employee.Profession.ManagerID == CurrentEmployee().ID && ex.Status == "pending").Select(n => new NeedRequestDTO
            {
                ID = n.ID,
                EmployeeName = n.Employee.Name,
                CategoryName = n.needsCategory.Name,
                SubCategoryName = n.subCategory.SubCategoryName,
                NeedRequestDate = n.NeedRequestDate,
                Status = n.Status,
                Comment = n.Comment
            }).ToList();
            // return needrequest;
            return needrequest;
        }
        [Route("GetPendingNeedRequest")]
        public IEnumerable<NeedRequestDTO> GetPendingNeedRequest()
        {
            var needrequest = _context.NeedsRequests.Where(ex =>ex.Status == "pending").Select(n => new NeedRequestDTO
            {
                ID = n.ID,
                EmployeeName = n.Employee.Name,
                CategoryName = n.needsCategory.Name,
                SubCategoryName = n.subCategory.SubCategoryName,
                NeedRequestDate = n.NeedRequestDate,
                Status = n.Status,
                Comment = n.Comment
            }).ToList();
            // return needrequest;
            return needrequest;
        }

        [Route("GetApprovedNeedRequestByManager")]
        public IEnumerable<NeedRequestDTO> GetApprovedNeedRequestByManager()
        {
            var needrequest = _context.NeedsRequests.Where(ex => ex.Employee.Profession.ManagerID == CurrentEmployee().ID && ex.Status == "approved").Select(n => new NeedRequestDTO
            {
                ID = n.ID,
                EmployeeName = n.Employee.Name,
                CategoryName = n.needsCategory.Name,
                SubCategoryName = n.subCategory.SubCategoryName,
                NeedRequestDate = n.NeedRequestDate,
                Status = n.Status,
                Comment = n.Comment
            }).ToList();
            // return needrequest;
            return needrequest;
        }
        [Route("GetApprovedNeedRequest")]
        public IEnumerable<NeedRequestDTO> GetApprovedNeedRequest()
        {
            var needrequest = _context.NeedsRequests.Where(ex => ex.Status == "approved").Select(n => new NeedRequestDTO
            {
                ID = n.ID,
                EmployeeName = n.Employee.Name,
                CategoryName = n.needsCategory.Name,
                SubCategoryName = n.subCategory.SubCategoryName,
                NeedRequestDate = n.NeedRequestDate,
                Status = n.Status,
                Comment = n.Comment
            }).ToList();
            // return needrequest;
            return needrequest;
        }

        [Route("GetDisApprovedNeedRequestByManager")]
        public IEnumerable<NeedRequestDTO> GetDisApprovedNeedRequestByManager()
        {
            var needrequest = _context.NeedsRequests.Where(ex => ex.Employee.Profession.ManagerID == CurrentEmployee().ID && ex.Status == "disapproved").Select(n => new NeedRequestDTO
            {
                ID = n.ID,
                EmployeeName = n.Employee.Name,
                CategoryName = n.needsCategory.Name,
                SubCategoryName = n.subCategory.SubCategoryName,
                NeedRequestDate = n.NeedRequestDate,
                Status = n.Status,
                Comment = n.Comment
            }).ToList();
            // return needrequest;
            return needrequest;
        }
        [Route("GetDisApprovedNeedRequest")]
        public IEnumerable<NeedRequestDTO> GetDisApprovedNeedRequest()
        {
            var needrequest = _context.NeedsRequests.Where(ex => ex.Status == "disapproved").Select(n => new NeedRequestDTO
            {
                ID = n.ID,
                EmployeeName = n.Employee.Name,
                CategoryName = n.needsCategory.Name,
                SubCategoryName = n.subCategory.SubCategoryName,
                NeedRequestDate = n.NeedRequestDate,
                Status = n.Status,
                Comment = n.Comment
            }).ToList();
            // return needrequest;
            return needrequest;
        }

        [Route("AcceptNeedRequest/{id}")]
        public ActionResult AcceptNeedRequest(int id)
        {
            var needsRequest = _context.NeedsRequests.Find(id);

            if (needsRequest == null)
            {
                return NotFound();
            }
            needsRequest.Status = "approved";
            _context.SaveChanges();
            return Ok();
        }
        [Route("RejectNeedRequest/{id}")]
        public ActionResult RejectNeedRequest(int id)
        {
            var needsRequest = _context.NeedsRequests.Find(id);
            if (needsRequest == null)
            {
                return NotFound();
            }
            needsRequest.Status = "disapproved";
            _context.SaveChanges();
            return Ok();
        }


        [HttpGet]
        // [Route("api/TypesNeedsRequests")]
        [Route("GetNeedRequestCategories")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IEnumerable<string> GetNeedRequestTypes()
        {
            var type = _context.NeedsRequests.Select(n => n.needsCategory.Name).ToList();
            return type;
        }

        // GET: api/NeedRequestDTOes/5
        [HttpGet("{id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public NeedRequestDTO GetNeedRequestDTO(int id)
        {
           // var needRequestDTO = await _context.NeedRequestDTO.FindAsync(id);
            var need = _context.NeedsRequests.Include(n => n.Employee).Include(n=>n.subCategory).Include(n=>n.needsCategory).FirstOrDefault(n => n.ID == id);
            var needRequest = new NeedRequestDTO
            {
                ID = need.ID,
                EmployeeName = need.Employee.Name,
                EmployeeId = need.EmployeeID,
                CategoryName = need.needsCategory.Name,
                CategoryId=need.CategoryID,
                SubCategoryName=need.subCategory.SubCategoryName,
                SubCategoryId=need.SubCategoryID,
                NeedRequestDate =need.NeedRequestDate,
                Status = need.Status,
                Comment = need.Comment,
            };
            return needRequest;
        }
        [HttpGet]
        [Route("GetNeedRequestsByCategoryId/{categoryId}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public List<NeedRequestDTO> GetNeedRequestsByCategoryId(int categoryId)
        {
            return _context.NeedsRequests.Where(a => a.CategoryID == categoryId).Select(item =>
          new NeedRequestDTO
          {
              ID = item.ID,
              CategoryId = item.CategoryID,
              CategoryName =item.needsCategory.Name,
              SubCategoryName = item.subCategory.SubCategoryName,
              EmployeeName=item.Employee.Name,
              NeedRequestDate=item.NeedRequestDate,
              Status=item.Status

          }).ToList();
        }


        [HttpGet]
        [Route("GetNeedRequestsBySubCategoryId/{SubCategoryId}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public List<NeedRequestDTO> GetNeedRequestsBySubCategoryId(int SubCategoryId)
        {
            return _context.NeedsRequests.Where(a => a.SubCategoryID == SubCategoryId).Select(item =>
          new NeedRequestDTO
          {
              ID = item.ID,
              CategoryId = item.CategoryID,
              CategoryName = item.needsCategory.Name,
              SubCategoryName = item.subCategory.SubCategoryName,
              EmployeeName = item.Employee.Name,
              NeedRequestDate = item.NeedRequestDate,
              Status=item.Status

          }).ToList();
        }
        // PUT: api/NeedRequestDTOes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<NeedsRequest>> PutNeedRequestDTO(int id, NeedRequestViewModel needRequestViewModel)
        {
            if (id != needRequestViewModel.ID)
            {
                return BadRequest();
            }
            NeedsRequest needsRequest = new NeedsRequest
            {
                ID = needRequestViewModel.ID,
                EmployeeID = needRequestViewModel.EmployeeID,
                CategoryID = needRequestViewModel.CategoryID,
                SubCategoryID=needRequestViewModel.SubCategoryID,
                NeedRequestDate = needRequestViewModel.NeedRequestDate,
                Status=needRequestViewModel.Status,
                Comment = needRequestViewModel.Comment
            };
            _context.Entry(needsRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NeedRequestDTOExists(id))
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
        // POST: api/NeedRequestDTOes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<NeedsRequest>> PostNeedRequestDTO(NeedRequestViewModel needRequestViewModel)
        {
            NeedsRequest needsRequest = new NeedsRequest
            {
                ID = needRequestViewModel.ID,
                EmployeeID = needRequestViewModel.EmployeeID,
                CategoryID = needRequestViewModel.CategoryID,
                SubCategoryID=needRequestViewModel.SubCategoryID,
                NeedRequestDate = needRequestViewModel.NeedRequestDate,
                Status=needRequestViewModel.Status,
                Comment = needRequestViewModel.Comment
            };
            _context.NeedsRequests.Add(needsRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNeedRequestDTO", new { id = needRequestViewModel.ID }, needRequestViewModel);
        }
        [Route("PostNeedRequestEmp")]
        public async Task<ActionResult<NeedsRequest>> PostNeedRequestEmpDTO(NeedRequestViewModel needRequestViewModel)
        {
            //System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            //var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            //var emp = _context.Employees.Where(e => e.Email == email).FirstOrDefault();
            //needRequestViewModel.EmployeeID = emp.ID;
            NeedsRequest needsRequest = new NeedsRequest
            {
                ID = needRequestViewModel.ID,
                EmployeeID = needRequestViewModel.EmployeeID,
                CategoryID = needRequestViewModel.CategoryID,
                SubCategoryID = needRequestViewModel.SubCategoryID,
                NeedRequestDate = needRequestViewModel.NeedRequestDate,
                Status=needRequestViewModel.Status,
                Comment = needRequestViewModel.Comment
            };
            _context.NeedsRequests.Add(needsRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNeedRequestDTO", new { id = needRequestViewModel.ID }, needRequestViewModel);
        }

        // DELETE: api/NeedRequestDTOes/5
        [HttpDelete("{id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<NeedsRequest>> DeleteNeedRequestDTO(int id)
        {
            var needRequestDTO = await _context.NeedsRequests.FindAsync(id);
            if (needRequestDTO == null)
            {
                return NotFound();
            }

            _context.NeedsRequests.Remove(needRequestDTO);
            await _context.SaveChangesAsync();

            return needRequestDTO;
        }

        private bool NeedRequestDTOExists(int id)
        {
            return _context.NeedsRequests.Any(e => e.ID == id);
        }
    }
}
