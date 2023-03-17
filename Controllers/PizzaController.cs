using Microsoft.AspNetCore.Mvc;
using Pizzas.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pizzas.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class PizzasController : ControllerBase {
        [HttpGet]
        public IActionResult GetAll() {
            IActionResult response = null;
            List<Pizza> listaPizza;

            listaPizza = BD.GetAll();
            response = Ok(listaPizza);
            return response;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            IActionResult response = null;
            Pizza pizza;
            pizza = BD.GetById(id);
            if (pizza == null) {
                response = NotFound();
            } else {
                response = Ok(pizza);
            }
            return response;
        }

        [HttpPost]
        public IActionResult Create(Pizza pizza) {
            BD.Insert(pizza);
            return CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza pizza) {
            IActionResult response = null;
            Pizza entity;
            int intRowsAffected;
            if (id != pizza.Id) {
                response =  BadRequest();
            } else {
                entity = BD.GetById(id);
                if (entity == null){
                response = NotFound();
                } else {
                    intRowsAffected = BD.UpdateById(pizza);
                    if (intRowsAffected > 0){
                        response = Ok(pizza);
                    } else {
                        response = NotFound();
                    }
                }
            }
            return response;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id) {
            IActionResult response = null;
            Pizza pizza;
            int intRowsAffected;

            pizza = BD.GetById(id);
            if (pizza == null){
                response = NotFound();
            } else {
                intRowsAffected = BD.DeleteById(id);
                if (intRowsAffected > 0){
                    response = Ok(pizza);
                } else {
                    response = NotFound();
                }
            }
            return response;
        }
    }
}