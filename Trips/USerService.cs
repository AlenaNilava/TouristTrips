using System;

namespace Trips
{
	class UserService
	{
		public static string purposeParameter;
		public static string transportParameter;
		public static string foodTypeParameter;
		public static int durationParameter;

		public static void InitilizeUserInfo()
		{
			do
			{
				Console.Write("Which purpose of your trip? (sea, excursion, shopping, health) : ");
				try
				{
					purposeParameter = Console.ReadLine();
					if (!purposeParameter.ToLower().Equals("sea") && !purposeParameter.ToLower().Equals("excursion") && !purposeParameter.ToLower().Equals("shopping") && !purposeParameter.ToLower().Equals("health"))
					{
						throw new InvalidUserInputException("Invalid purpose. Try again.");
					}
				}
				catch (InvalidUserInputException e)
				{
					Console.WriteLine(e.Message);
					purposeParameter = null;
				}
			}
			while (purposeParameter == null);
			do
			{
				Console.Write("Which transport you prefer? (plain, bus, train) : ");
				try
				{
					transportParameter = Console.ReadLine();
					if (!transportParameter.ToLower().Equals("plain") && !transportParameter.ToLower().Equals("bus") && !transportParameter.ToLower().Equals("train"))
					{
						throw new InvalidUserInputException("Invalid transport. Try again.");
					}
				}
				catch (InvalidUserInputException e)
				{
					Console.WriteLine(e.Message);
					transportParameter = null;
				}
			}
			while (transportParameter == null);
			do
			{
				Console.Write("Which food type you prefer? (AI, BC, BB) : ");
				try
				{
					foodTypeParameter = Console.ReadLine();
					if (!foodTypeParameter.ToUpper().Equals("AI") && !foodTypeParameter.ToUpper().Equals("BC") && !foodTypeParameter.ToUpper().Equals("BB"))
					{
						throw new InvalidUserInputException("Invalid food type. Try again.");
					}
				}
				catch (InvalidUserInputException e)
				{
					Console.WriteLine(e.Message);
					foodTypeParameter = null;
				}
			}
			while (foodTypeParameter == null);
			do
			{
				Console.Write("Enter trip duration : ");
				try
				{
					durationParameter = Convert.ToInt32(Console.ReadLine());
				}
				catch (FormatException e)
				{
					e = new FormatException("Duration should be integer value.");
					Console.WriteLine(e.Message);
					durationParameter = 0;
				}
			}
			while (durationParameter == 0);
		}
	}
}
