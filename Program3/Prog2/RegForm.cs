// Grading ID: B9073
// Program 3
// CIS 199-75
// Due: 11/20/2016

// This application calculates the earliest registration date
// and time for an undergraduate student given their credit hours
// and last name.
// Decisions based on UofL Fall/Summer 2016 Priority Registration Schedule

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prog2
{
    public partial class RegForm : Form
    {
        public RegForm()
        {
            InitializeComponent();
        }

        const string TIME1 = "8:30 AM";  // 1st time block
        const string TIME2 = "10:00 AM"; // 2nd time block
        const string TIME3 = "11:30 AM"; // 3rd time block
        const string TIME4 = "2:00 PM";  // 4th time block
        const string TIME5 = "4:00 PM";  // 5th time block

        // Preconditions: Must enter a value of type char.
        // Postconditions: An appropriate string of time will be returned based upon char entered.
        private string GetJuniorsAndSeniorsTime(char lastName) // Creates method that returns a string value and accepts a char value
        {
            char[] name = { 'A', 'E', 'J', 'P', 'T' }; // Creates array of lower limit chars named name that represent student last name ranges
            string[] time = { TIME4, TIME5, TIME1, TIME2, TIME3 }; // Creates array of registration times named time
            bool found = false; // Creates boolean variable found and sets it equal to false
            string registrationTime = ""; //Assume no name for bad input

            int index = name.Length - 1; // Start from end
                                                          // since lower limits
            while (index >= 0 && !found) // Loop runs if index is greater than or equal to 0 and found value is true
            {
                if (lastName >= name[index])
                    found = true; // Boolean value is changed to true if input last name is greater than or equal to char value in name array
                else
                    --index; // If not last name is not greater than or equal to name array at index, index = index -1
            }

            if (found)
                registrationTime = time[index]; // If the loop finds a match in the array, it looks parallel to the time array and assigns value to registrationTime

            return registrationTime; // Method returns value registrationTime to whoever calls it.

        }

        // Preconditions: Must enter a value of type char.
        // Postconditions: An appropriate string of time will be returned based upon char entered.
        private string GetFreshmanAndSophomoresTime(char lastName) // Creates method that returns a string value and accepts a char value
        {
            char[] name = { 'A', 'C', 'E', 'G', 'J', 'M', 'P', 'R', 'T', 'W' }; // Creates array of lower limit chars named name that represent student last name ranges
            string[] time = { TIME2, TIME3, TIME4, TIME5, TIME1, TIME2, TIME3, TIME4, TIME5, TIME1 }; // Creates array of registration times named time
            bool found = false; // Creates boolean variable found and sets it equal to false
            string registrationTime = ""; //Assume no name for bad input

            int index = name.Length - 1; // Start from end
                                         // since lower limits

            while (index >= 0 && !found) // Loop runs if index is greater than or equal to 0 and found value is true
            {
                if (lastName >= name[index])
                    found = true; // Boolean value is changed to true if input last name is greater than or equal to char value in name array
                else
                    --index; // If not last name is not greater than or equal to name array at index, index = index -1
            }
            if (found)
                 registrationTime = time[index]; // If the loop finds a match in the array, it looks parallel to the time array and assigns value to registrationTime
            return registrationTime; // Method returns value registrationTime to whoever calls it.        
        }

        private void findRegTimeBtn_Click(object sender, EventArgs e)
        {
            const float SENIOR_HOURS = 90;    // Min hours for Senior
            const float JUNIOR_HOURS = 60;    // Min hours for Junior
            const float SOPHOMORE_HOURS = 30; // Min hours for Soph.

            const string DAY1 = "November 4";  // 1st day of registration
            const string DAY2 = "November 7";  // 2nd day of registration
            const string DAY3 = "November 9";  // 3rd day of registration
            const string DAY4 = "November 10"; // 4th day of registration
            const string DAY5 = "November 11"; // 5th day of registration
            const string DAY6 = "November 14"; // 6th day of registration

            string lastNameStr;       // Entered last name
            char lastNameLetterCh;    // First letter of last name, as char
            string dateStr = "Error"; // Holds date of registration
            string timeStr = "Error"; // Holds time of registration
            float creditHours;        // Entered credit hours

            if (float.TryParse(creditHrTxt.Text, out creditHours) && creditHours >= 0) // Valid hours
            {
                lastNameStr = lastNameTxt.Text;
                if (lastNameStr.Length > 0) // Empty string?
                {
                    lastNameLetterCh = lastNameStr[0];   // First char of last name
                    lastNameLetterCh = char.ToUpper(lastNameLetterCh); // Ensure upper case

                    if (char.IsLetter(lastNameLetterCh)) // Is it a letter?
                    {
                        // Juniors and Seniors share same schedule but different days
                        if (creditHours >= JUNIOR_HOURS)
                        {
                            if (creditHours >= SENIOR_HOURS)
                                dateStr = DAY1;
                            else // Must be juniors
                                dateStr = DAY2;

                           timeStr = GetJuniorsAndSeniorsTime(lastNameLetterCh); // Calls method GetJuniorAndSeniorsTime and assigns its return to timeStr
                            
                        }
                        // Sophomores and Freshmen
                        else // Must be soph/fresh
                        {
                            if (creditHours >= SOPHOMORE_HOURS)
                            {
                                // J-V on one day
                                if ((lastNameLetterCh >= 'J') && // >= J and
                                    (lastNameLetterCh <= 'V'))   // <= V
                                    dateStr = DAY3;
                                else // All other letters on next day
                                    dateStr = DAY4;
                            }
                            else // must be freshman
                            {
                                // J-V on one day
                                if ((lastNameLetterCh >= 'J') && // >= J and
                                    (lastNameLetterCh <= 'V'))   // <= V
                                    dateStr = DAY5;
                                else // All other letters on next day
                                    dateStr = DAY6;
                            }

                            timeStr = GetFreshmanAndSophomoresTime(lastNameLetterCh); // Calls method GetFreshmanAndSophomoresTime and assigns its return to timeStr
                        }

                        // Output results
                        dateTimeLbl.Text = dateStr + " at " + timeStr;
                    }
                    else // First char not a letter
                        MessageBox.Show("Enter valid last name!");
                }
                else // Empty textbox
                    MessageBox.Show("Enter a last name!");
            }
            else // Can't parse credit hours
                MessageBox.Show("Please enter valid credit hours earned!");
        }
    }
}
