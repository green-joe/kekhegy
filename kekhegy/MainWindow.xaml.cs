using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace kekhegy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string connectText = "datasource=127.0.0.1;port=3306;username=root;password=1234;database=kekhegy;";
        private MySqlConnection connect;
        private List<Room> data;
        private List<string> idOfRoom = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            data = new List<Room>();
            connect = new MySqlConnection(connectText);
            connect.Open();
            string queryText = "SELECT emelet,szobaszam,ferohelyek,ar,megjegyzes FROM szobak";
            MySqlCommand query = new MySqlCommand(queryText, connect);

            query.CommandTimeout = 60;

            try
            {
                MySqlDataReader reader = query.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {


                        Room room = new Room
                        {
                            Emelet = reader.GetInt32("emelet"),
                            Szobaszam = reader.GetInt32("szobaszam"),
                            Ferohelyek = reader.GetInt32("ferohelyek"),
                            Ar = reader.GetDecimal("ar"),
                            Megjegyzes = reader.IsDBNull(4) ? "" : reader.GetString("megjegyzes")
                        };
                        data.Add(room);
                    }
                }
                reader.Close();
            }
            catch (Exception hiba)
            {
                MessageBox.Show(hiba.Message);
            }
            finally
            {
                listing.ItemsSource = data;

            }
            connect.Close();

        } 
        
        private void button_Click(object sender, RoutedEventArgs e)
        {
            int chosen = listing.SelectedIndex;
            MessageBox.Show(chosen.ToString());
            data.RemoveAt(chosen);
            listing.Items.Refresh();
            string deleteText = $"DELETE FROM szobak WHERE szobaszam={data[chosen].Szobaszam}";
            MessageBox.Show(deleteText);
            connect = new MySqlConnection(connectText);
            connect.Open();
            MySqlCommand deleting = new MySqlCommand(deleteText, connect);
            deleting.CommandTimeout = 60;
            deleting.ExecuteNonQuery();
            connect.Close();
        }     
               
        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabControl.SelectedIndex == 1)
            {
                connect = new MySqlConnection(connectText);
                connect.Open();
                string queryText = "SELECT concat(emelet, szobaszam) FROM szobak";
                MySqlCommand query = new MySqlCommand(queryText, connect);
                query.CommandTimeout = 60;
                try
                {
                    MySqlDataReader reader = query.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            string roomID = reader.GetString(0);
                            combobox.Items.Add(roomID);
                            idOfRoom.Add(roomID);

                        }

                    }

                    reader.Close();

                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }

                connect.Close();
            }

        }

        private string selectedRoomNumber=null;
        private void combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {           
            if (combobox.SelectedIndex!=0)
            {
                selectedRoomNumber = combobox.SelectedItem.ToString();
            }   
        } 
        private void NumbersOfDaysChecker(string numbersOfDays)
        {
            string compare = numbersOfDays;
            bool isOk = false;
            Regex regex = new Regex(@"^\d+$");

            if (regex.IsMatch(compare))
            {
                isOk = true;
            }               
            
            if ((numbersOfDays=="" || numbersOfDays == null) && !isOk)
            {
                throw new Exception("The number of days is incorrect!");
            }
        }
        private void SelectedRoomChecker()
        {
            try
            {
                
                while(combobox.SelectedIndex == 0 || selectedRoomNumber==null)
                {                   
                    throw new Exception();                    
                }                         
                              
            }
            catch (Exception)
            {
                throw new Exception("You didn't select a room!");            
            }
        } 
        int guestId;
        private int GetGuestId(string name)
        {

            try
            {
                string queryGuestId = $"SELECT id FROM vendegek WHERE nev='{name}'";
                MySqlCommand queryGuest = new MySqlCommand(queryGuestId, connect);
                MySqlDataReader reader = queryGuest.ExecuteReader();
                reader.Read();
                guestId = reader.GetInt32(0);
                reader.Close();
                return guestId;
            } catch (Exception)
            {
                throw new Exception("This guest doesn't exist!");
            }

        }

        int roomId = 0;
        private int GetRoomId(int floor,int roomNumber)
        {
            
            try
            {
                floor = int.Parse(selectedRoomNumber.Substring(0, 1));
                roomNumber = int.Parse(selectedRoomNumber.Substring(1));
                string queryRoomId = $"SELECT id FROM szobak WHERE emelet={floor} and szobaszam={roomNumber}";
                MySqlCommand queryRoom = new MySqlCommand(queryRoomId, connect);
                return roomId = (int)queryRoom.ExecuteScalar();
            } catch (Exception)
            {
                throw new Exception("You didn't select a room number or it doesn't exist!");
            }
        }
        private void booking_button_Click(object sender, RoutedEventArgs e)
        {            
            string numberOfDays = numberofDaysText.Text;
            string name= nameText.Text;
            string datestring = null;
            try
            {
                datestring = dateText.SelectedDate.Value.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception )
            {
                MessageBox.Show("Date cannot be null!");
            }
            
            connect = new MySqlConnection(connectText);
            connect.Open();            
            
            try
            {
                
                GetGuestId(name);                 
                SelectedRoomChecker();

                int floor = int.Parse(selectedRoomNumber.Substring(0, 1));
                int roomNumber = int.Parse(selectedRoomNumber.Substring(1));
                
                GetRoomId(floor,roomNumber);
                NumbersOfDaysChecker(numberOfDays);

                string saveBookingText = $"INSERT INTO foglalasok (szobaid, vendegid, napok, datum) VALUES ({roomId},{guestId},{numberOfDays},'{datestring}')";
                MySqlCommand saveBooking = new MySqlCommand(saveBookingText, connect);
                saveBooking.ExecuteNonQuery();
                MessageBox.Show("Booking saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            connect.Close();
        }

       
    }
}
      

    

