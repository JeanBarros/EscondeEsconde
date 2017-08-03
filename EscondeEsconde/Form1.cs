using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EscondeEsconde
{
    public partial class Form1 : Form
    {
        int Moves;
        Location currenteLocation;
        RoomWithDoor livingRoom;
        RoomWithHidingPlace diningRoom;
        RoomWithDoor kitchen;
        Room stairs;
        RoomWithHidingPlace hallway;
        RoomWithHidingPlace bathroom;
        RoomWithHidingPlace masterBedroom;
        RoomWithHidingPlace secondBedroom;
        OutsideWithDoor frontYard;
        OutsideWithDoor backYard;
        OutsideWithHidingPlace garden;
        OutsideWithHidingPlace driveway;
        
        Oponent opponent;
        
        public Form1()
        {
            InitializeComponent();
            CreateObjects();
            opponent = new Oponent(frontYard);
            ResetGame(false);
        }

        private void ResetGame(bool displayMessage)
        {
            if (displayMessage)
            {
                MessageBox.Show("You found me in " + Moves + "moves!");
                IHidingPlace foundLocation = currenteLocation as IHidingPlace;
                description.Text = "You found your opponent in " + Moves
                    + " moves! He was hiding " + foundLocation.HidingPlaceName + ".";
            }
            Moves = 0;
            hide.Visible = true;
            goHere.Visible = false;
            check.Visible = false;
            goThrougTheDoor.Visible = false;
            exits.Visible = false;
        }

        private void CreateObjects()
        {
            livingRoom = new RoomWithDoor("Living room", "an antique carpet", 
                "inside the cleset",  "an oak door with a brass handle");
            diningRoom = new RoomWithHidingPlace("Dining room", "a cristal chandelier", 
                "in the tall armoire");
            kitchen = new RoomWithDoor("Kitchen", "stainless steel appliances", 
                "in the cabinet", "a screen door");
            stairs = new Room("Stairs", "a wooden bannister");
            hallway = new RoomWithHidingPlace("upstairs hallway", "a picture of a dog", "in the closet");
            bathroom = new RoomWithHidingPlace("Bathroom", "a sink and a toilet", "in the shower");
            masterBedroom = new RoomWithHidingPlace("Master Bedroom", "a large bed", "under the bed");
            secondBedroom = new RoomWithHidingPlace("Second bedrrom", "a small bed", "under the bed");
            frontYard = new OutsideWithDoor("Front yard", false, "a heavy-looking oak door");
            backYard = new OutsideWithDoor("Back yard", true, "a screen door");
            garden = new OutsideWithHidingPlace("Garden", false, "inside the shed");
            driveway = new OutsideWithHidingPlace("Driveway", true, "in the garage");

            diningRoom.Exits = new Location[] { livingRoom, kitchen };
            livingRoom.Exits = new Location[] { diningRoom, stairs };            
            kitchen.Exits = new Location[] { diningRoom };
            stairs.Exits = new Location[] { livingRoom, hallway };
            hallway.Exits = new Location[] { stairs, bathroom, masterBedroom, secondBedroom };
            bathroom.Exits = new Location[] { hallway };
            masterBedroom.Exits = new Location[] { hallway };
            secondBedroom.Exits = new Location[] { hallway };
            frontYard.Exits = new Location[] { backYard, garden, driveway };
            backYard.Exits = new Location[] { frontYard, garden, driveway };
            garden.Exits = new Location[] { backYard, frontYard };
            driveway.Exits = new Location[] { backYard, frontYard };            

            livingRoom.DoorLocation = frontYard;
            frontYard.DoorLocation = livingRoom;
            kitchen.DoorLocation = backYard;
            backYard.DoorLocation = kitchen;
        }

        private void MoveToANewLocation(Location newLocation)
        {
            currenteLocation = newLocation;
            exits.Items.Clear();

            for (int i = 0; i < currenteLocation.Exits.Length; i++)
                exits.Items.Add(currenteLocation.Exits[i].Name);
            exits.SelectedIndex = 0;

            description.Text = currenteLocation.Description;
            if (currenteLocation is IHasExteriorDoor)
                goThrougTheDoor.Visible = true;
            else
                goThrougTheDoor.Visible = false;

        }

        private void goHere_Click(object sender, EventArgs e)
        {
            MoveToANewLocation(currenteLocation.Exits[exits.SelectedIndex]);
        }

        private void goThrougTheDoor_Click(object sender, EventArgs e)
        {
            IHasExteriorDoor hasDoor = currenteLocation as IHasExteriorDoor;
            MoveToANewLocation(hasDoor.DoorLocation);
        }        
    }
}
