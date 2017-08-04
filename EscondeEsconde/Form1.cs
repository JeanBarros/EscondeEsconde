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
                MessageBox.Show("Você me encontrou em " + Moves + " movimentos!");
                IHidingPlace foundLocation = currenteLocation as IHidingPlace;
                description.Text = "Você encontrou o oponente em " + Moves
                    + " movimentos! Ele estava escondido em " + foundLocation.HidingPlaceName + ".";
            }
            Moves = 0;
            hide.Visible = true;
            goHere.Visible = false;
            check.Visible = false;
            goThrougTheDoor.Visible = false;
            exits.Visible = false;
        }

        private void check_Click(object sender, EventArgs e)
        {
            Moves++;
            if (opponent.Check(currenteLocation))
                ResetGame(true);
            else
                RedrawForm();
        }

        private void hide_Click(object sender, EventArgs e)
        {
            hide.Visible = false;
            for (int i=1;i<=10;i++)
            {
                opponent.Move();
                description.Text = i + "... ";
                Application.DoEvents();
                System.Threading.Thread.Sleep(1300);
            }
            description.Text = "Pronto ou não, aí vou eu!";
            Application.DoEvents();
            System.Threading.Thread.Sleep(2000);
            goHere.Visible = true;
            exits.Visible = true;
            MoveToANewLocation(livingRoom);
        }

        private void RedrawForm()
        {
            exits.Items.Clear();
            for (int i = 0; i < currenteLocation.Exits.Length; i++)
                exits.Items.Add(currenteLocation.Exits[i].Name);
            exits.SelectedIndex = 0;
            description.Text = currenteLocation.Description +
                "\r\n(move #" + Moves + ")";
            if (currenteLocation is IHidingPlace)
            {
                IHidingPlace hidingPlace = currenteLocation as IHidingPlace;
                check.Text = "Check " + hidingPlace.HidingPlaceName;
                check.Visible = true;
            }
            else
                check.Visible = false;
            if (currenteLocation is IHasExteriorDoor)
                goThrougTheDoor.Visible = true;
            else
                goThrougTheDoor.Visible = false;
        }

        private void CreateObjects()
        {
            livingRoom = new RoomWithDoor("sala", "um tapete antigo", 
                "dentro do closet", "uma porta de carvalho com uma maçaneta de latão");
            diningRoom = new RoomWithHidingPlace("sala de jantar", "um candelabro de cristal", 
                "no armário alto");
            kitchen = new RoomWithDoor("cozinha", "aparelhos de aço inoxidável", 
                "no armário embaixo da pia", "uma porta com uma tela");
            stairs = new Room("escada", "um corrimão de madeira");
            hallway = new RoomWithHidingPlace("corredor do andar de cima", "um quadro de um cachorro", "no closet");
            bathroom = new RoomWithHidingPlace("banheiro", "uma pia e um vaso", "no chuveiro");
            masterBedroom = new RoomWithHidingPlace("quarto de casal", "uma cama grande", "debaixo da cama");
            secondBedroom = new RoomWithHidingPlace("quarto de solteiro", "uma cama pequena", "debaixo da cama");
            frontYard = new OutsideWithDoor("quintal da frente", false, "uma porta de carvalho pesado");
            backYard = new OutsideWithDoor("quintal dos fundos", true, "uma porta com uma tela");
            garden = new OutsideWithHidingPlace("jardim", false, "dentro do galpão");
            driveway = new OutsideWithHidingPlace("calçada", true, "na garagem");

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
            Moves++;
            currenteLocation = newLocation;
            RedrawForm();
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
