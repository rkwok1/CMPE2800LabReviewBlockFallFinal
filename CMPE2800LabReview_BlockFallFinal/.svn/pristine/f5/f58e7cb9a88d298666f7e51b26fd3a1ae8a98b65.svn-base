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
    class GridManager
    {
        //----------------------------------Field Member----------------------------
        int i_GridXSize;                 //Dimensions of the x Axis grid
        int i_GridYSize;                 //Dimensions of the y Axis grid
        int i_BlockSize;                 //Size of the blocks
        Block[,] b_BlockGrid;            //Grid that blocks are placed on
        PlaceHolder[,] p_PlaceHolderGrid; //Grid that keeps track of where the placeholder blocks are

        //Constructor that creates the Block Grid, and PlaceHolder Grid
        public GridManager(int m_x, int m_y, int m_dim)
        {
            i_GridXSize = m_x;
            i_GridYSize = m_y;
            i_BlockSize = m_dim;
            b_BlockGrid = new Block[m_x, m_y];                            //Creation of Grid when the main form passes it to cosntructor
            p_PlaceHolderGrid = new PlaceHolder[i_GridXSize, i_GridYSize]; //Creation of PlaceHolder Grid
        }

        //----------------------------------Methods----------------------------------
        //Generates a block that is capable of falling
        public void AddLiveBlock(Point pos)
        {
            if (b_BlockGrid[pos.X, pos.Y] == null && p_PlaceHolderGrid[pos.X, pos.Y] == null)
            {
                Rectangle r_Dimensions = new Rectangle(pos.X, pos.Y, i_BlockSize, i_BlockSize);
                b_BlockGrid[pos.X, pos.Y] = new Block(r_Dimensions, Color.Red, GridObject.State.Live);
            }
        }
        //Generates a block that does not fall and remains in place
        public void AddStationaryBlock(Point pos)
        {
            if (b_BlockGrid[pos.X, pos.Y] == null && p_PlaceHolderGrid[pos.X, pos.Y] == null)
            {
                Rectangle r_Dimensions = new Rectangle(pos.X, pos.Y, i_BlockSize, i_BlockSize);
                b_BlockGrid[pos.X, pos.Y] = new Block(r_Dimensions, Color.Orange, GridObject.State.Stationary);
            }
        }


        //Begins the killing of a block
        public void KillBlock(Point pos)
        {
            if (b_BlockGrid[pos.X, pos.Y] != null)
            {
                b_BlockGrid[pos.X, pos.Y].s_State = GridObject.State.Dying;
            }
        }
        //Check for states during timer tick
        public void TimerTick(CDrawer dr)
        {
            dr.Clear();
            for (int x = 0; x < i_GridXSize; x++)
            {
                for (int y = 0; y < i_GridYSize; y++)
                {
                    if (b_BlockGrid[x, y] != null)
                    {
                        switch (b_BlockGrid[x, y].s_State)
                        {
                            //If a block is live
                            case GridObject.State.Live:
                                Render(x, y, dr);
                                if (y < i_GridYSize - 1)
                                {
                                    //If block below is empty and within range, then switch the live state to falling
                                    if (b_BlockGrid[x, y + 1] == null)
                                    {
                                        b_BlockGrid[x, y].s_State = GridObject.State.SafeToFall;
                                    }
                                }
                                break;
                            //If block is stationary
                            case GridObject.State.Stationary:
                                //Simply render it to the drawer
                                Render(x, y, dr);
                                break;
                            case GridObject.State.Dying:
                                if (b_BlockGrid[x, y].i_ttlTimer <= 5)
                                {
                                    int xDim = b_BlockGrid[x, y].r_Dimensions.X * i_BlockSize + (i_BlockSize / 2);
                                    int yDim = b_BlockGrid[x, y].r_Dimensions.Y * i_BlockSize + (i_BlockSize / 2);
                                    int TTLDim = i_BlockSize - b_BlockGrid[x, y].i_ttlTimer;
                                    //Animate for dying state
                                    dr.AddCenteredRectangle(xDim, yDim, TTLDim, TTLDim, RandColor.GetColor(), 2, Color.Black);
                                    b_BlockGrid[x, y].i_ttlTimer++;
                                }
                                else
                                {
                                    //Remove block from pic
                                    b_BlockGrid[x, y] = null;
                                }
                                break;
                            case GridObject.State.SafeToFall:
                                //Place a placeholder below by one dimension
                                p_PlaceHolderGrid[x, y + 1] = new PlaceHolder(new Rectangle(1, 1, 1, 1));
                                b_BlockGrid[x, y].s_State = GridObject.State.Falling;
                                break;
                            case GridObject.State.Falling:
                                if (b_BlockGrid[x, y].moveAnimationCount <= 5)
                                {
                                    //Animate block as it falls by rendering it ahead
                                    int xDim = b_BlockGrid[x, y].r_Dimensions.X * i_BlockSize + (i_BlockSize / 2);
                                    int yDim = b_BlockGrid[x, y].r_Dimensions.Y * i_BlockSize + (i_BlockSize / 2) + 10 * b_BlockGrid[x, y].moveAnimationCount;
                                    dr.AddCenteredRectangle(xDim, yDim, i_BlockSize, i_BlockSize, Color.Red, 2, Color.Black);
                                    b_BlockGrid[x, y].moveAnimationCount++;
                                }
                                else
                                {
                                    //Remove falling block 
                                    b_BlockGrid[x, y] = null;
                                    //Replace with live block
                                    Rectangle bDim = new Rectangle(x, y + 1, i_BlockSize, i_BlockSize);
                                    b_BlockGrid[x, y + 1] = new Block(bDim, Color.Red, GridObject.State.Live);
                                    p_PlaceHolderGrid[x, y + 1] = null;
                                }
                                break;
                            default:
                                break;

                        }
                    }
                }
            }
            dr.Render();
        }
        //render method to draw each block
        public void Render(int x, int y, CDrawer dr)
        {
            dr.AddRectangle(b_BlockGrid[x, y].r_Dimensions.X * i_BlockSize, b_BlockGrid[x, y].r_Dimensions.Y * i_BlockSize, i_BlockSize, i_BlockSize, b_BlockGrid[x, y].c_Color, 2, Color.Black);
        }



    }
}
