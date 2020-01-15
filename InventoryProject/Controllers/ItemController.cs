using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryProject.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace InventoryProject.Controllers
{
    public class ItemController : Controller
    {
        string connStr = "server=automap.cxnllsgrt5yp.us-west-2.rds.amazonaws.com;uid=scott;pwd=kennedy;database=ScottsDB";
        public IActionResult Index()
        {
            List<Item> itemList = new List<Item>();
            string sqlStr = "";
            MySqlConnection connection = null;
            MySqlCommand cmd = null;
            try {
                //open a connection and check for any existing data on the database.
                connection = new MySqlConnection(connStr);
                connection.Open();
                sqlStr = "SELECT * FROM Items";
                cmd = new MySqlCommand(sqlStr, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //make new items to populate the table.
                    string iName = reader["itemName"].ToString();
                    string iNum = reader["itemNum"].ToString();
                    string iManufacturer = reader["Manufacturer"].ToString();
                    string iDescription = reader["Description"].ToString();
                    string iCategory = reader["Category"].ToString();
                    double iPrice = (double)reader["Price"];

                    Item AddItem = new Item(iName, iNum, iManufacturer, iDescription, iCategory, iPrice);
                    itemList.Add(AddItem);
                }


            }catch(Exception exc)
            {
                Console.WriteLine(exc);
            }
            if (null != connection)
                connection.Close();
                //closes connection once data is retrieved, if it's open.

            return View(itemList);
        }

        public IActionResult Create()
        {
            //create windows start here, so they can be blank.
            return View();
        }
        public IActionResult Remove(Item item)
        {
            //remove starts here so we can have a nice confirmation window.
            return View(item);
        }

        [HttpPost]
        public IActionResult Remove(string itemNum)
        {
            string sqlStr = "";
            MySqlConnection connection = null;
            MySqlCommand cmd = null;
            try
            {
                connection = new MySqlConnection(connStr);
                connection.Open();
                sqlStr = "DELETE FROM Items WHERE itemNum = '" + itemNum + "'";
                cmd = new MySqlCommand(sqlStr, connection);
                cmd.ExecuteNonQuery();

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);

            }
            //close connection
            if (null != connection)
                connection.Close();
            return RedirectToAction("Index");
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Edit(Item item)
        {
            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(string oldNumber, string itemName, string itemNum, string Manufacturer, string Description, string Category, double Price)
        {//oldNumber exists in case we end up editing the itemNumber so we can still find it in the database.
            if (ModelState.IsValid)
            {
                //attempt to connect to sql server.

                string sqlStr = "";
                MySqlConnection connection = null;
                MySqlCommand cmd = null;
                try
                {
                    connection = new MySqlConnection(connStr);
                    connection.Open();
                    sqlStr = "UPDATE Items SET itemNum = '" + itemNum + "', itemName = '" + itemName + "', Manufacturer = '" + Manufacturer + "', Description ='" + Description
                        + "', Category = '" + Category + "', Price = '" + Price + "' WHERE itemNum = '" + oldNumber + "'";
                    cmd = new MySqlCommand(sqlStr, connection);
                    cmd.ExecuteNonQuery();

                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc);

                }
                //close connection
                if (null != connection)
                    connection.Close();
                return RedirectToAction("Index");
            }
            Item item = new Item(itemName, itemNum, Manufacturer, Description, Category, Price);
            return View(item);
        }
        
        
        

        [HttpPost]
        public IActionResult Create(Item item)
        {//this does the actual heavy lifting for the creation.
            if(ModelState.IsValid)
            {
                //attempt to connect to sql server.

                string sqlStr = "";
                MySqlConnection connection = null;
                MySqlCommand cmd = null;
                try
                {
                    connection = new MySqlConnection(connStr);
                    connection.Open();
                    sqlStr = "INSERT INTO Items (itemNum, itemName, Manufacturer, Description, Category, Price) VALUES ('" + item.itemNum + "', '" + item.itemName + "', '" +
                    item.Manufacturer + "', '" + item.Description + "', '" + item.Category + "', '" + item.Price + "')";
                    cmd = new MySqlCommand(sqlStr, connection);
                    cmd.ExecuteNonQuery();

                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc);
                    
                }
                //close connection
                if (null != connection)
                    connection.Close();
                return RedirectToAction("Index");
            }
            return View(item);
        }


    }
}