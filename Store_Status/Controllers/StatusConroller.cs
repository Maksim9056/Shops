using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopClassLibrary;
using ShopClassLibrary.ModelShop;

namespace Store_Status.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusConroller: ControllerBase
    {
        private readonly ShopData _context;
        public StatusConroller(ShopData context)
        {
            _context = context;
        }

        [HttpGet("usercreate")]
        public async Task<Status> Status_User_Create()
        {
            Status status = new Status();
            try
            {
                var check = await  _context.Status.FirstOrDefaultAsync(u =>u.StatusName == "Новый пользователь");


                if (check == null )
                {

                }else
                {
                    status = check;
                }

            }
            catch (Exception)
            {

            }
            return status;
        }

        [HttpPost]
        public async Task StatusCreate([FromBody] Status Project)
        {

        }
    }
}
