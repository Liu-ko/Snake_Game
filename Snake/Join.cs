using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    /// <summary>
    /// Middle class to construct Snake object. It holds one bone and jones around it: Next join and previous one. 
    /// </summary>
    public class Join
    {
        private Bone bone;
        private Join next;
        private Join prev;

        /// <summary>
        /// Basic constructor without parameters
        /// </summary>
        public Join()
        {
            bone = null;
            next = null;
            prev = null;
        }

        /// <summary>
        /// Constructor that receives just Bone object as a parameter
        /// </summary>
        /// <param name="b">Bone object</param>
        public Join(Bone b)
        {
            bone = b;
            next = null;
            prev = null;
        }

        /// <summary>
        /// Constructor with all parameters to be set
        /// </summary>
        /// <param name="bone">Bone object itself</param>
        /// <param name="previous">Join object with previous bone</param>
        /// <param name="next">Join object with next bone</param>
        public Join(Bone bone, Join previous, Join next)
        {
            this.bone = bone;
            this.next = next;
            prev = previous;
        }

        /// <summary>
        /// Method to get Join object with next Bone
        /// </summary>
        /// <returns>Join object with next Bone</returns>
        public Join getNext()
        {
            return next;
        }

        /// <summary>
        /// Method to get Join object with previous Bone
        /// </summary>
        /// <returns>Join object with previous Bone</returns>
        public Join getPrev()
        {
            return prev;
        }

        /// <summary>
        /// Method to get Bone object in this Join
        /// </summary>
        /// <returns>Bone object</returns>
        public Bone getBone()
        {
            return bone;
        }

        /// <summary>
        /// Method to set Join object with next Bone
        /// </summary>
        /// <param name="next">Join object that goes before current</param>
        public void setNext(Join next)
        {
            this.next = next;
        }

        /// <summary>
        /// Method to set Join object with previous Bone
        /// </summary>
        /// <param name="previous">Join object that goes after current</param>
        public void setPrev(Join previous)
        {
            prev = previous;
        }

        /// <summary>
        /// Method to set Bone in this Join
        /// </summary>
        /// <param name="bone">Bone to the set</param>
        public void setBone(Bone bone)
        {
            this.bone = bone;
        }

        /// <summary>
        /// Method that checks if this Join has next Join after
        /// </summary>
        /// <returns>true if next Join not equals to null</returns>
        public Boolean hasNext()
        {
            return (next != null);
        }
    }
}
