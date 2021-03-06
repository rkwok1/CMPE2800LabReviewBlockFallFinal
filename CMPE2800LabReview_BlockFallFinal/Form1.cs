﻿/**************************************************************************************************************************
   * Program: LabReview - BlockFall
   * Lastest Revision Date: Jan 21, 2018
   * Author: Ryan Kwok
   * 
   * Abstract Classes: GridObject
   * Concrete Classes: Block, PlaceHolder,GridManager
   * 
   * Description: The following program allows a user to click in a GDIdrawer window to create blcoks that may fall unless 
   *              it comes into contact with another block or the bottom of the window.A user may right click to kill a
   *              block, removing it from the window. The user may also hold the shift key, and right click to place a
   *              stationary block that does not move.
   * *********************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GDIDrawer;

namespace CMPE2800LabReview_BlockFallFinal
{

    public partial class Form1 : Form
    {
        //Main Form Field Members
        const int i_BlockSize = 100;                                                                  //Represents the dimensions of each block
        const int i_BlocksPerRow = 10;                                                               //Represents the number of blocks that will fill in a window in a row
        const int i_BlocksPerColumn = 10;                                                            //Represents the number of blocks within a column in a window
        const int i_WindowSizeX = i_BlockSize * i_BlocksPerRow;                                      //Window X-Axis size is determined by the size of each block and the number per row
        const int i_WindowSizeY = i_BlockSize * i_BlocksPerColumn;                                   //Window Y-Axis size is determined by the size of each block and the number per column
        CDrawer _dr = new CDrawer(i_WindowSizeX, i_WindowSizeY, false, true);                        //Drawer Window
        GridManager g_GridManager = new GridManager(i_BlocksPerRow, i_BlocksPerColumn, i_BlockSize); //Grid properties for the particular grid dimenions
        public Form1()
        {
            InitializeComponent();
            //Mouse Event Subscriptions
            _dr.MouseLeftClick += _dr_MouseLeftClick;
            _dr.MouseRightClick += _dr_MouseRightClick;
        }

        //-------------------------------Form Events-----------------------------


        private void _dr_MouseLeftClick(Point pos, CDrawer dr)
        {
            Invoke(new GDIDrawerMouseEvent(ActualMouseLeft), pos, dr);
        }
        private void _dr_MouseRightClick(Point pos, CDrawer dr)
        {
            Invoke(new GDIDrawerMouseEvent(ActualMouseRight), pos, dr);
        }

        //Invoking events to prevent cross thread collisions
        //When the left mouse button is clicked...
        private void ActualMouseLeft(Point pos, CDrawer dr)
        {

            //To maintain within grid
            int pointX = pos.X / i_BlockSize;
            int pointY = pos.Y / i_BlockSize;
            Point gridPoint = new Point(pointX, pointY);
            //Pass to gridmanager to generate a live block
            g_GridManager.AddLiveBlock(gridPoint);

        }

        private void ActualMouseRight(Point pos, CDrawer dr)
        {
            //If the shift key is held  and a right mouseclick is detected, create a stationary block
            if (Control.ModifierKeys == Keys.Shift)
            {
                int pointX = pos.X / i_BlockSize;
                int pointY = pos.Y / i_BlockSize;
                Point gridPoint = new Point(pointX, pointY);
                g_GridManager.AddStationaryBlock(gridPoint);
            }
            //Otherwise, kill target block and prepare dying animation
            else
            {
                int pointX = pos.X / i_BlockSize;
                int pointY = pos.Y / i_BlockSize;
                Point gridPoint = new Point(pointX, pointY);
                g_GridManager.KillBlock(gridPoint);
            }

        }
        //Every tick of the timer causes movement for dying and falling states, and rendering of blocks
        private void UI_Timer_Tick(object sender, EventArgs e)
        {
            g_GridManager.TimerTick(_dr);
        }
    }
}
