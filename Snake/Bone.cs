using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// This class contains coordinates of each Snake bone and it's direction. 
/// It is the smallest, base class to construct Snake object. 
/// </summary>


namespace Snake
{
    public class Bone
    {
        private int dir; // direction
        private Coordinates cor; // coordinates of the bone on a "board"

        #region constructors

        /// <summary>
        /// Constructor without parameters
        /// </summary>
        public Bone() 
        {
            dir = 0;
            cor = new Coordinates(0, 0);
        }
        
        /// <summary>
        /// Constructor with 2 parameters, coordinates of the bone
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Bone(int x, int y)
        {
            cor = new Coordinates(x, y);
            dir = 0;
        }
        /// <summary>
        /// Constructor with 3 parameters, coordinates of the bone and it's direction
        /// </summary>
        /// <param name="b">Direction that this bone should follow</param>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Bone(int b, int x, int y)
        {
            cor = new Coordinates(x, y);
            dir = b;
        }

        #endregion

        /// <summary>
        /// Method to get Coordinates of the bone
        /// </summary>
        /// <returns>Coordinates object with coordinates of the bone</returns>
        public Coordinates getCoord() 
        {
            return cor;
        }

        /// <summary>
        /// Method to set Coordinates of the bone
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public void setCoord(int x, int y)
        {
            cor.setX(x);
            cor.setY(y);
        }

        /// <summary>
        /// Method to get bone's direction
        /// </summary>
        /// <returns>int with bone's direction</returns>
        public int getDirection()
        {
            return dir;
        }

        /// <summary>
        /// Method to set bone's direction
        /// </summary>
        /// <param name="b">Directioin that bone follows</param>
        public void setDirection(int b)
        {
            dir = b;
        }
    }
}
