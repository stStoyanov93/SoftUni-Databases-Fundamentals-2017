﻿using System;
using P01_HospitalDatabase.Data;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using(var db = new HospitalContext())
            {
                //to initialise, use provided generator
                db.Database.EnsureDeleted();                
            }
        }
    }
}
