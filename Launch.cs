using System;
using System.Net.Http;           // For HttpClient
using System.Net.NetworkInformation; // For Network Connectivity Check
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace Emailch
{
    public partial class Launch : Form
    {
        private Timer emailCheckTimer;
        private static readonly HttpClient httpClient = new HttpClient();

        public Launch()
        {
            InitializeComponent();
            emailCheckTimer = new Timer();
            emailCheckTimer.Interval = 2000; // 2000 milliseconds = 2 seconds
            emailCheckTimer.Tick += emailtimer1_Tick;

            // Subscribe to the TextBox's KeyPress event
            //email_txt.KeyPress += email_txt_KeyPress;
        }

        private void Launch_Load(object sender, EventArgs e)
        {

        }

        private void Launch_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        //private void email_txt_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    // Restart the timer on every key press
        //    emailtimer1.Stop();  // Stop the timer first
        //    emailtimer1.Start(); // Start the timer again
        //}

        private void emailtimer1_Tick(object sender, EventArgs e)
        {
            //// Stop the timer to avoid multiple checks
            //emailtimer1.Stop();

            //// Check if the entered email is valid
            //if (IsValidEmail(email_txt.Text))
            //{
            //    MessageBox.Show("Valid email address!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else
            //{
            //    MessageBox.Show("Invalid email address!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
        //Email validation method using Regex
        private bool IsValidEmail(string email)
        {
            // Define a simple regex for validating email
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsValidEmail(email_txt.Text))
            {
                MessageBox.Show("Valid email address!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Invalid email address!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void email_txt_Keyup(object sender, KeyEventArgs e)
        {
            button1.PerformClick();
        }

        private async  void email_txt_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key was pressed
            if (e.KeyCode == Keys.Enter)
            {
                // Trigger the button click event
                button1.PerformClick(); // Automatically "press" the button
                e.SuppressKeyPress = true; // Optional: to prevent the beep sound or newline insertion
                // Check if the network is available
                if (IsNetworkAvailable())
                {
                    // Call the method to check the email
                    await CheckEmail(email_txt.Text);
                }
                else
                {
                    MessageBox.Show("No network connection. Please check your connection and try again.",
                                    "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool IsNetworkAvailable()
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply = ping.Send("8.8.8.8", 1000); // Pinging Google DNS server
                  return reply.Status == IPStatus.Success;
                }

               // tss_net.Text = IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }

        private async Task CheckEmail(string email)
        {
            // Ensure TLS 1.2 is used
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please enter an email.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate email format
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Invalid email format.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Create the request body
                var requestBody = new { email = email };
                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(requestBody),
                                                System.Text.Encoding.UTF8,
                                                "application/json");

                // Make the POST request to the new API endpoint
                HttpResponseMessage response = await httpClient.PostAsync("https://throwaway.cloud/api/v2/email/", content);

                // Check the response status code
                if (response.IsSuccessStatusCode)
                {
                    // Parse the API response
                    string responseContent = await response.Content.ReadAsStringAsync();
                    // Update the RichTextBox with the response content
                    richTextBox1.Text = $"API Response: {responseContent}";
                }
                else
                {
                    // Handle the error response
                    string errorContent = await response.Content.ReadAsStringAsync();
                    //MessageBox.Show($"Error connecting to the server: {response.StatusCode}\n{errorContent}", "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    richTextBox1.Text =$"{ errorContent}";
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error connecting to the server: {ex.Message}", "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
