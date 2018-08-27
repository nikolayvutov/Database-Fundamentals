﻿using System;
using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data;

namespace P01_HospitalDatabase
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (HospitalContext context = new HospitalContext())
            {
                context.Database.Migrate();
            }
        }
    }
}