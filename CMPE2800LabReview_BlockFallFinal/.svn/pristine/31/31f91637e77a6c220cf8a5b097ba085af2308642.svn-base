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
    class Block : GridObject  //Derivative of Grid Object
    {
        //--------------------------Field Member----------------------
        public int i_ttlTimer = 0;         //Timer for how long a block has to live
        public int moveAnimationCount = 0; //Counter for move animation
        
        //Constructor for block that passes to gird object. no additional features added
        public Block(Rectangle dimensions, Color color, State state)
            :base(dimensions,color,state)
        {

        }
        //Equals Override
        public override bool Equals(object obj)
        {
            if (!(obj is Block))
            {
                return false;
            }

            Block temp = (Block)obj;

            //If the block intersects with another block, then they are overlapping which we are considering as logical equality
            return this.r_Dimensions.IntersectsWith(temp.r_Dimensions);
        }

        //Get Hash Code override
        public override int GetHashCode()
        {
            return 1;
        }
    }
}
