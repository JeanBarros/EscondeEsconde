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
        Location currenteLocation;
        RoomWithDoor livingRoom;
        Room diningRoom;
        RoomWithDoor kitchen;
        OutsideWithDoor frontYard;
        OutsideWithDoor backYard;
        Outside garden;
        Room stairs;
        RoomWithHidingPlace hallway;
        RoomWithHidingPlace masterRoom;
        RoomWithHidingPlace secondRoom;
        RoomWithHidingPlace bathroom;

        public Form1()
        {
            InitializeComponent();
            CreateObjects();
            MoveToANewLocation(livingRoom);
        }

        private void CreateObjects()
        {
            livingRoom = new RoomWithDoor("Living room", "an antique carpet", "an oak door with a brass knob");
            diningRoom = new Room("Dining room", "a cristal chandelier");
            kitchen = new RoomWithDoor("Kitchen", "stainless steel appliances", "a screen door");
            frontYard = new OutsideWithDoor("Front yard", false, "an oak door with a brass knob");
            backYard = new OutsideWithDoor("Back yard", true, "a screen door");
            garden = new Outside("Garden", false);
            stairs = new Room("Stairs", "a wooden bannister");
            hallway = new RoomWithHidingPlace("upstairs hallway", "a picture of a dog and a closet");
            masterRoom = new RoomWithHidingPlace("Master room", "a large bed");


            livingRoom.Exits = new Location[] { diningRoom };
            diningRoom.Exits = new Location[] { livingRoom, kitchen };
            kitchen.Exits = new Location[] { diningRoom };
            frontYard.Exits = new Location[] { backYard, garden };
            backYard.Exits = new Location[] { frontYard, garden };
            garden.Exits = new Location[] { frontYard, backYard };
            stairs.Exits = new Location[] { hallway };
            hallway.Exits = new Location[] { stairs };

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
