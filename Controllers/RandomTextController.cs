using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RandomTextCase.Models;
using RandomTextCase.SqlHelper;

namespace RandomTextCase.Controllers
{
    public class RandomTextController : Controller
    {

        private readonly IUserDataRepository _userRepository;

        public RandomTextController(IUserDataRepository userRepository)
        {
            _userRepository = userRepository;
        }

        int stringToIntValue;
        

        // GET: /<controller>/
        public IActionResult RandomText()
        {
            //ViewBag.Words = _userRepository.GetList();


            var model = new TextViewModel();
            model.input = new Input();
            model.list = _userRepository.GetList();
            return View(model);
        }



        [HttpPost]
        public IActionResult RandomText(TextViewModel model)
        {

            if (string.IsNullOrEmpty(model.input.inputText))
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = "input text can not null!" });
            }

            stringToIntValue = Int32.Parse(model.input.inputText);


            var allowedChars = "abcçdefgğhiıjklmnoöprsştuüvyz";

            Random random = new Random();

            char[] chars = new char[stringToIntValue];

            for (int j = 1; j < 3; j++)
            {
                for (int i = 0; i < stringToIntValue; i++)
                {
                    chars[i] = allowedChars[random.Next(0, allowedChars.Length)];
                }
                string charToString = new string(chars);

                model.input.inputText = charToString;

                /// db ye charToString kaydet.
                var response = _userRepository.InsertData(model.input);

                if (response.Success)
                {
                    ViewBag.Message = response.Message;
                    
                }
                else
                {
                    
                    
                    return View("Error",new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = response.Message });
                }
               

            }
            model.input = new Input();
            model.list = _userRepository.GetList();
            return View(model);
        }

       


    }
}

