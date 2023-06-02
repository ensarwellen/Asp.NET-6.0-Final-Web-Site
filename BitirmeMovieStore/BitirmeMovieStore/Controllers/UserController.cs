using AutoMapper;
using BitirmeMovieStore.Entities;
using BitirmeMovieStore.Models;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;

namespace BitirmeMovieStore.Controllers
{
    public class UserController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;


        public UserController(DatabaseContext databaseContext, IMapper mapper, IConfiguration configuration)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            List<UserModel> users = _databaseContext.Users.ToList().Select(x=> _mapper.Map<UserModel>(x)).ToList();
            return View(users);
        }
        private string DoMD5HashedString(string s)
        {
            string md5Salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
            string salted = s + md5Salt;
            string hashed = salted.MD5();
            return hashed;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateUserModel model)
        {
            if(ModelState.IsValid)
            {
                //Veritabanında bu kullanıcı adına sahip başka bir kullanıcı var mı?
                if (_databaseContext.Users.Any(x=>x.Username.ToLower()==model.Username.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.Username),"Username is already exists.");
                    return View(model);
                }
                string hashedPassword = DoMD5HashedString(model.Password);
                User user= _mapper.Map<User>(model);
                user.Password = hashedPassword;
                _databaseContext.Users.Add(user);
                _databaseContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        public IActionResult Edit(Guid id)
        {
            User user = _databaseContext.Users.Find(id);
            EditUserModel model = _mapper.Map<EditUserModel>(user);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Guid id, EditUserModel model)
        {
            if (ModelState.IsValid)
            {
                //Kullanıcının kendi haricindeki diğer kullanıcılarla kıyaslama yapılır
                if (_databaseContext.Users.Any(x => x.Username.ToLower() == model.Username.ToLower() && x.Id != id))
                {
                    ModelState.AddModelError(nameof(model.Username), "Username is already exists.");
                    return View(model);
                }
                User user = _databaseContext.Users.Find(id);
                _mapper.Map(model, user);    //Modeldekileri user'a at
                _databaseContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        
        public IActionResult Delete(Guid id)
        {
           
            User user = _databaseContext.Users.Find(id);

            if(user != null)
            {
                _databaseContext.Users.Remove(user);
                _databaseContext.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
