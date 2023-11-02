using System;
using System.Text.RegularExpressions;

namespace DVDL_Business.Global
{

    public class ValidationHelper
    {
        public class StringValidator
        {
            // Validate if a string is not null or empty
            public static bool IsNotNullOrEmpty(string input)
            {
                return !string.IsNullOrEmpty(input);
            }

            // Validate if a string is a valid URL
            public static bool IsUrl(string url)
            {
                if (Uri.TryCreate(url, UriKind.Absolute, out Uri result))
                {
                    return result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps;
                }
                return false;
            }

            // Validate if a string is a valid phone number (supports various formats)
            public static bool IsPhoneNumber(string phoneNumber)
            {
                // Use a regular expression for phone number validation
                string phonePattern = @"^(?:(?:\+?(\d{1,3}))?[-.\s]?)?\(?(?=\d{1,4}\))(?:(\d{1,4})\)?[-.\s]?)(\d{1,4})[-.\s]?(\d{1,9})$";
                return Regex.IsMatch(phoneNumber, phonePattern);
            }

            // Validate if a string contains only lowercase letters
            public static bool IsLowerCase(string text)
            {
                return Regex.IsMatch(text, "^[a-z]+$");
            }

            // Validate if a string contains only uppercase letters
            public static bool IsUpperCase(string text)
            {
                return Regex.IsMatch(text, "^[A-Z]+$");
            }

            // Validate if a string contains at least one uppercase letter
            public static bool ContainsUpperCaseLetter(string text)
            {
                return Regex.IsMatch(text, "[A-Z]");
            }

            // Validate if a string contains at least one lowercase letter
            public static bool ContainsLowerCaseLetter(string text)
            {
                return Regex.IsMatch(text, "[a-z]");
            }

            // Validate if a string contains at least one digit
            public static bool ContainsDigit(string text)
            {
                return Regex.IsMatch(text, "[0-9]");
            }

            // Validate if a string contains at least one special character
            public static bool ContainsSpecialCharacter(string text)
            {
                return Regex.IsMatch(text, "[!@#$%^&*()_+\\-=\\[\\]{};':\",.<>?]");
            }

            // Validate if a string is a valid ZIP code (US)
            public static bool IsZipCode(string zipCode)
            {
                // Use a regular expression for US ZIP code validation
                string zipPattern = @"^\d{5}(?:-\d{4})?$";
                return Regex.IsMatch(zipCode, zipPattern);
            }

            // Validate if a string is a valid IP address (IPv4)
            public static bool IsIpAddress(string ipAddress)
            {
                // Use a regular expression for IPv4 address validation
                string ipPattern = @"^(\d{1,3}\.){3}\d{1,3}$";
                return Regex.IsMatch(ipAddress, ipPattern);
            }

            // Validate if a string is a valid email address
            public static bool IsEmail(string email)
            {
                // Use a regular expression for email address validation
                string emailPattern = @"^[\w\.-]+@[\w\.-]+\.\w+$";
                return Regex.IsMatch(email, emailPattern);
            }

            public static bool ContainsNumbersOnly(string text)
            {
                return Regex.IsMatch(text, "^[0-9]+$");
            }

            public static bool IsNotNullOrEmpty(object imagePath)
            {
                throw new NotImplementedException();
            }
        }

        public class NumberValidator
        {
            // Validate if a number is positive
            public static bool IsPositive(int number)
            {
                return number > 0;
            }

            // Validate if a number is negative
            public static bool IsNegative(int number)
            {
                return number < 0;
            }

            // Validate if a number is even
            public static bool IsEven(int number)
            {
                return number % 2 == 0;
            }

            // Validate if a number is odd
            public static bool IsOdd(int number)
            {
                return number % 2 != 0;
            }

        }

        public class DateValidator
        {
            // Validate if a date is in the past
            public static bool IsDateInPast(DateTime date)
            {
                return date < DateTime.Now;
            }

            // Validate if a date is in the future
            public static bool IsDateInFuture(DateTime date)
            {
                return date > DateTime.Now;
            }

            // Validate if a date falls within a specified date range
            public static bool IsDateInRange(DateTime date, DateTime minDate, DateTime maxDate)
            {
                return date >= minDate && date <= maxDate;
            }

            // Validate if a date is a weekend (Saturday or Sunday)
            public static bool IsWeekend(DateTime date)
            {
                return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
            }

            // Validate if a date is a weekday (Monday through Friday)
            public static bool IsWeekday(DateTime date)
            {
                return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
            }

            // Validate if a year is a leap year
            public static bool IsLeapYear(int year)
            {
                return DateTime.IsLeapYear(year);
            }

            // Validate if a date is today's date
            public static bool IsToday(DateTime date)
            {
                return date.Date == DateTime.Now.Date;
            }

            // Validate if a date is in the current month
            public static bool IsCurrentMonth(DateTime date)
            {
                return date.Year == DateTime.Now.Year && date.Month == DateTime.Now.Month;
            }

            // Validate if a date is a valid date (e.g., not December 32nd)
            public static bool IsValidDate(int year, int month, int day)
            {
                try
                {
                    DateTime date = new DateTime(year, month, day);
                    return true;
                }
                catch (ArgumentOutOfRangeException)
                {
                    return false;
                }
            }

            // Validate if a date is the first day of the month
            public static bool IsFirstDayOfMonth(DateTime date)
            {
                return date.Day == 1;
            }

            // Validate if a date is the last day of the month
            public static bool IsLastDayOfMonth(DateTime date)
            {
                return date.Day == DateTime.DaysInMonth(date.Year, date.Month);
            }

            // Validate if two dates match (are equal)
            public static bool AreDatesEqual(DateTime date1, DateTime date2)
            {
                return date1.Date == date2.Date;
            }
            // Validate if date1 is before date2
            public static bool IsDate1BeforeDate2(DateTime date1, DateTime date2)
            {
                return date1 < date2;
            }
            // Increase a date by one day
            public static DateTime IncreaseDateByOneDay(DateTime date)
            {
                return date.AddDays(1);
            }

            // Decrease a date by one day
            public static DateTime DecreaseDateByOneDay(DateTime date)
            {
                return date.AddDays(-1);
            }

        }

    }


}

