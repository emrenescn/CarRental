using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //CarTest();
           

        }
        private static void CarTest()
        {
            ICarDal carDal = new EfCarDal();
            CarManager carManager = new CarManager(carDal);
            foreach (Car car in carManager.GetAll().Data)
            {
                Console.WriteLine(car.ModelYear);
            }
        }
    }
}
