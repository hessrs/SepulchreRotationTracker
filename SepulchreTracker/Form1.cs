using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SepulchreTracker {
    public partial class sepulchredRotationCounter : Form {

        String rotationString = "";
        int currentFloor = 1;
        bool onReset = false;


        readonly int numberOfFloorsWithMultiplePaths = 4;
        readonly String ENTRANCE_DIRECTION_WEST = "w";
        readonly String ENTRANCE_DIRECTION_EAST = "e";

        public sepulchredRotationCounter() {
            InitializeComponent();
        }

        private void sepulchreRotationCounter_Load(object sender, EventArgs e) {

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            westEntranceN.Click += buttonClick;
            westEntranceE.Click += buttonClick;
            westEntranceS.Click += buttonClick;
            westEntranceW.Click += buttonClick;
            eastEntranceN.Click += buttonClick;
            eastEntranceE.Click += buttonClick;
            eastEntranceS.Click += buttonClick;
            eastEntranceW.Click += buttonClick;

            westEntranceN.FlatAppearance.BorderColor = Color.Black;
            westEntranceN.FlatAppearance.BorderSize = 1;
            westEntranceE.FlatAppearance.BorderColor = Color.Black;
            westEntranceE.FlatAppearance.BorderSize = 1;
            westEntranceS.FlatAppearance.BorderColor = Color.Black;
            westEntranceS.FlatAppearance.BorderSize = 1;
            westEntranceW.FlatAppearance.BorderColor = Color.Black;
            westEntranceW.FlatAppearance.BorderSize = 1;
            eastEntranceN.FlatAppearance.BorderColor = Color.Black;
            eastEntranceN.FlatAppearance.BorderSize = 1;
            eastEntranceE.FlatAppearance.BorderColor = Color.Black;
            eastEntranceE.FlatAppearance.BorderSize = 1;
            eastEntranceS.FlatAppearance.BorderColor = Color.Black;
            eastEntranceS.FlatAppearance.BorderSize = 1;
            eastEntranceW.FlatAppearance.BorderColor = Color.Black;
            eastEntranceW.FlatAppearance.BorderSize = 1;

        }
        private void buttonReset_Click(object sender, EventArgs e) {

            writeRotationToFile();
            currentFloor = 1;
            rotationString = "";
            labelFloor.Text = "Floor: " + currentFloor;

            enableAppropriateDirectionsForFloor(currentFloor, "RESET");
        }

        private void buttonClick(object src, EventArgs args) {


            Button clickedButton = (src as Button);
            String clickedButtonName = clickedButton.Name.ToLower();
            String direction = clickedButton.Name.Substring(clickedButton.Name.Length - 1, 1);
            String entranceLobbyDirection = clickedButtonName.Substring(0, 1);

            if (currentFloor == 1) {
                rotationString += entranceLobbyDirection; // inserts whether we took the west or east entrance
            }

            // + 1 to account for the lobby direction
            if (rotationString.Length < (numberOfFloorsWithMultiplePaths + 1)) {
                rotationString += direction;
            }
            else {
                Console.WriteLine("Could not add direction since you already finished floor " + (numberOfFloorsWithMultiplePaths) + ".");
                Console.WriteLine("Current Rotation String: " + rotationString);
                Console.WriteLine("Current floor: " + currentFloor);
                Console.WriteLine("---");
            }
            
            if (currentFloor >= numberOfFloorsWithMultiplePaths) {
                /*currentFloor = numberOfFloorsWithMultiplePaths;*/
            }

            currentFloor++;
            
            enableAppropriateDirectionsForFloor(currentFloor, entranceLobbyDirection);

            Console.WriteLine("Current Rotation String: " + rotationString);
            Console.WriteLine("Current floor: " + currentFloor);
            labelFloor.Text = "Floor: " + currentFloor;
        }

        private void writeRotationToFile() {

            // + 1 to account for the lobby direction
            if (rotationString.Length != (numberOfFloorsWithMultiplePaths + 1)) { 
                Console.WriteLine("rotationString \"" + rotationString + "\" is not 4 characters long.");
                return;
			}

            try {
                StreamWriter file = new System.IO.StreamWriter(System.IO.Directory.GetCurrentDirectory() + "/sepulchreRotations.txt", true);
                file.WriteLine(rotationString);
                file.Close();
            }
            catch (IOException e) {
                MessageBox.Show(e.ToString());
			}
        }

        private void enableAppropriateDirectionsForFloor(int floor, String lobbyEntranceDirection) {
            
            if (floor >= 5) {
                disableAllDirectionButtons();
			}

            if (lobbyEntranceDirection.Equals("RESET")) { 
                westEntranceN.Enabled = true;
                westEntranceE.Enabled = true;
                westEntranceS.Enabled = true;
                westEntranceW.Enabled = true;

                eastEntranceN.Enabled = true;
                eastEntranceE.Enabled = true;
                eastEntranceS.Enabled = true;
                eastEntranceW.Enabled = true;

                return;
            }

            if (floor == 1) {
                if (lobbyEntranceDirection.Equals(ENTRANCE_DIRECTION_WEST)) {
                    westEntranceN.Enabled = true;
                    westEntranceE.Enabled = true;
                    westEntranceS.Enabled = true;
                    westEntranceW.Enabled = true;
                    eastEntranceN.Enabled = false;
                    eastEntranceE.Enabled = false;
                    eastEntranceS.Enabled = false;
                    eastEntranceW.Enabled = false;
                }
                else {
                    westEntranceN.Enabled = false;
                    westEntranceE.Enabled = false;
                    westEntranceS.Enabled = false;
                    westEntranceW.Enabled = false;
                    eastEntranceN.Enabled = true;
                    eastEntranceE.Enabled = true;
                    eastEntranceS.Enabled = true;
                    eastEntranceW.Enabled = true;
                }
            }
            else if (floor == 2) {
                if (lobbyEntranceDirection.Equals(ENTRANCE_DIRECTION_WEST)) {
                    westEntranceN.Enabled = true;
                    westEntranceE.Enabled = true;
                    westEntranceS.Enabled = true;
                    westEntranceW.Enabled = true;

                    eastEntranceN.Enabled = false;
                    eastEntranceE.Enabled = false;
                    eastEntranceS.Enabled = false;
                    eastEntranceW.Enabled = false;
                }
                else {
                    westEntranceN.Enabled = false;
                    westEntranceE.Enabled = false;
                    westEntranceS.Enabled = false;
                    westEntranceW.Enabled = false;

                    eastEntranceN.Enabled = true;
                    eastEntranceE.Enabled = true;
                    eastEntranceS.Enabled = true;
                    eastEntranceW.Enabled = true;
                }
            }
            else if (floor == 3) {
                if (lobbyEntranceDirection.Equals(ENTRANCE_DIRECTION_WEST)) {
                    westEntranceN.Enabled = false;
                    westEntranceE.Enabled = true;
                    westEntranceS.Enabled = false;
                    westEntranceW.Enabled = true;

                    eastEntranceN.Enabled = false;
                    eastEntranceE.Enabled = false;
                    eastEntranceS.Enabled = false;
                    eastEntranceW.Enabled = false;
                }
                else {
                    westEntranceN.Enabled = false;
                    westEntranceE.Enabled = false;
                    westEntranceS.Enabled = false;
                    westEntranceW.Enabled = false;

                    eastEntranceN.Enabled = false;
                    eastEntranceE.Enabled = true;
                    eastEntranceS.Enabled = false;
                    eastEntranceW.Enabled = true;
                }
            }
            else if (floor == 4) {
                if (lobbyEntranceDirection.Equals(ENTRANCE_DIRECTION_WEST)) {
                    westEntranceN.Enabled = true;
                    westEntranceE.Enabled = false;
                    westEntranceS.Enabled = true;
                    westEntranceW.Enabled = false;

                    eastEntranceN.Enabled = false;
                    eastEntranceE.Enabled = false;
                    eastEntranceS.Enabled = false;
                    eastEntranceW.Enabled = false;
                }
                else {
                    westEntranceN.Enabled = false;
                    westEntranceE.Enabled = false;
                    westEntranceS.Enabled = false;
                    westEntranceW.Enabled = false;

                    eastEntranceN.Enabled = true;
                    eastEntranceE.Enabled = false;
                    eastEntranceS.Enabled = true;
                    eastEntranceW.Enabled = false;
                }
            }
            else if (floor == 5) {
                
            }
        }

        private void disableAllDirectionButtons() {
            westEntranceN.Enabled = false;
            westEntranceE.Enabled = false;
            westEntranceS.Enabled = false;
            westEntranceW.Enabled = false;

            eastEntranceN.Enabled = false;
            eastEntranceE.Enabled = false;
            eastEntranceS.Enabled = false;
            eastEntranceW.Enabled = false;
        }

		private void openLogLocationToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                Process.Start(@System.IO.Directory.GetCurrentDirectory() + "/sepulchreRotations.txt");
            }
            catch (Exception) {
                MessageBox.Show("Error:\n\n" + System.IO.Directory.GetCurrentDirectory() + "/sepulchreRotations.txt" + " does not exist.\n\n" +
                    "Please either create the file at the above location, or complete a run and the program will automatically create the file for you.",
                    "Error");
            }
		}

		private void howToUseToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBox.Show(
                "When doing the Hallowed Sepulchre, when you spawn on a new floor, click the button correlated to the PATH you will be taking.\n\n" +
                "When you are done with your run, aka you are on floor 5, click the Reset button, which will save your progress and allow you to repeat.\n\n" +
                "Clicking the Reset button without making it to floor 5 will not track your progress.",
                "How to use"
            );
        }

		private void contactToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBox.Show(
                "You can contact me here:\n\n" +
                "twitter.com/Im_Hess\n" +
                "calcusource.com/contact",
                "Contact"
              
            );
        }

		private void alwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e) {
            this.TopMost = !(this.TopMost);
		}
	}
}
