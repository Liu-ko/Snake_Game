using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        private PictureBox[,] picb = new PictureBox[20, 15]; //grid of objects to be interacted with
        private Image[] images = new Image[15]; // images fot the first snake
        private Image[] images2; // images for the second snake if needed
        private Snake snake; // first snake
        private int dir; //direction that first snake should follow
        private Snake snake2; // second snake
        private int dir2; //direction that second snake should follow
        private static int points; //points collected overall
        private Timer step; // timer to control snake's movement
        private Timer time; // timer for the game to count time player plays
        private int min, sec; // time for timer
        private int x = 15, y = 10; // size of the game's grid
        private Boolean iftwo; // true if 2 players game was chosen, false if 1 Player game
        private Queue<int> queue; // Queue of instructions where the first snake should go
        private Queue<int> queue2; // Queue of instructions where the second snake should go
        private System.Media.SoundPlayer sound;
        private Boolean changes = false;
        private Image rock;

        #region fill form

        /// <summary>
        /// Constructor of the Form1 object
        /// </summary>
        /// <param name="npl">Number of players chosen</param>
        public Form1(int npl) // npl - number of players
        {
            InitializeComponent();
            iftwo = (npl == 2); // if game with two players was chosen. Will be used to check if additional operations should be performed

            try
            {
                sound = new System.Media.SoundPlayer(Properties.Resources.eat);
            }
            catch (Exception e)
            {
                sound = null;
            }

            if (iftwo)
            {
                images2 = new Image[15];
                loadImages2();
                snake2 = new Snake(x - 1, y - 1);
                dir2 = 4;
                queue2 = new Queue<int>();
            }

            step = new Timer();
            time = new Timer();

            loadImages();
            snake = new Snake(0, 0);
            dir = 6;
            points = 0;
            panel1_fill();
            putFood();
            queue = new Queue<int>();

            try
            {
                rock = ((Image)Properties.Resources.rock);
                DialogResult result = MessageBox.Show("Do you want to edit the playground?", "Important Question", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    changes = true;
                    MessageBox.Show("Press a cell to leave a rock, and press one more time to take it out.");
                    Button buton = new Button();
                    buton.Font = new System.Drawing.Font("Playbill", 30F);
                    buton.Location = new System.Drawing.Point(208, 340);
                    buton.Size = new System.Drawing.Size(55, 28);
                    buton.Text = "Start";
                    buton.AutoSize = true;
                    buton.Click += new System.EventHandler(start);
                    this.Controls.Add(buton);
                    buton.UseVisualStyleBackColor = true;
                }
                else
                {
                    timeOn();
                }
            }
            catch (Exception e) {}
        }

        /// <summary>
        /// Method that starts the game after you made changes on the grid
        /// </summary>
        /// <param name="sender">Start button</param>
        /// <param name="e"></param>
        private void start(object sender, System.EventArgs e)
        {
            this.Controls.Remove((Button)sender);
            timeOn();
            changes = false;
        }

        /// <summary>
        /// Starts timers and send the essage to inform user about start f the game
        /// </summary>
        private void timeOn()
        {
            MessageBox.Show("Let the game begin !");

            step.Tick += new EventHandler(onTick);
            step.Interval = 500;
            step.Start();

            time.Tick += new EventHandler(timerOn);
            time.Interval = 1000;
            min = 0; sec = 0;
            time.Start();
        }

        /// <summary>
        /// loands images for the first snake from game resources and saves it inside the program
        /// </summary>
        private void loadImages()
        {
            try
            {
                images[0] = ((Image)Properties.Resources._46);
                images[1] = ((Image)Properties.Resources._68);
                images[2] = ((Image)Properties.Resources.h2);
                images[3] = ((Image)Properties.Resources._48);
                images[4] = ((Image)Properties.Resources.h4);
                images[5] = ((Image)Properties.Resources._28);
                images[6] = ((Image)Properties.Resources.h6);
                images[7] = ((Image)Properties.Resources._26);
                images[8] = ((Image)Properties.Resources.h8);
                images[9] = ((Image)Properties.Resources._24);
                images[10] = ((Image)Properties.Resources.apple);
                images[11] = ((Image)Properties.Resources.b2);
                images[12] = ((Image)Properties.Resources.b4);
                images[13] = ((Image)Properties.Resources.b6);
                images[14] = ((Image)Properties.Resources.b8);
            }
            catch (Exception e)
            {
                MessageBox.Show("Sorry! Snake was not caught. We can not start playing without snake. Please try to catch it again.");
                time.Stop();
                step.Stop();
                Form form = Application.OpenForms[0];
                form.Show();
                this.Close();
            }
        }

        /// <summary>
        /// loands images for the second snake from game resources and saves it inside the program
        /// </summary>
        private void loadImages2()
        {
            try
            {
                images2[0] = ((Image)Properties.Resources.r46);
                images2[1] = ((Image)Properties.Resources.r68);
                images2[2] = ((Image)Properties.Resources.rh2);
                images2[3] = ((Image)Properties.Resources.r48);
                images2[4] = ((Image)Properties.Resources.rh4);
                images2[5] = ((Image)Properties.Resources.r28);
                images2[6] = ((Image)Properties.Resources.rh6);
                images2[7] = ((Image)Properties.Resources.r26);
                images2[8] = ((Image)Properties.Resources.rh8);
                images2[9] = ((Image)Properties.Resources.r24);
                images2[11] = ((Image)Properties.Resources.rb2);
                images2[12] = ((Image)Properties.Resources.rb4);
                images2[13] = ((Image)Properties.Resources.rb6);
                images2[14] = ((Image)Properties.Resources.rb8);
            }
            catch (Exception e)
            {
                MessageBox.Show("Sorry! Snake was not caught. We can not start playing without snake. Please try to catch it again.");
                time.Stop();
                step.Stop();
                Form form = Application.OpenForms[0];
                form.Show();
                this.Close();
            }
        }

        /// <summary>
        /// Fill the form with objects that will hold pictures during the game proccess
        /// </summary>
        private void panel1_fill()
        {
            int bsize = 30;
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                {
                    picb[i, j] = new PictureBox();
                    picb[i, j].Size = new Size(bsize, bsize);
                    picb[i, j].Location = new System.Drawing.Point(bsize * i, bsize * j);
                    picb[i, j].Image = null;
                    picb[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    picb[i, j].Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
                    picb[i, j].MouseEnter += new System.EventHandler(enterSpace);
                    picb[i, j].MouseLeave += new System.EventHandler(leaveSpace);
                    picb[i, j].Click += new System.EventHandler(putRock);
                    panel1.Controls.Add(picb[i, j]);
                }
            // put pictures of the first snake on a "gameboard"
            Join current = snake.getHead();
            setHeadLabel(current.getBone(), images);
            while (current.hasNext())
            {
                current = current.getNext();
                if (current.hasNext())
                    setBoneLabel(current.getBone(), images);
                else setTailLabel(current.getBone(), images);
            }

            if (iftwo)
            {// put pictures of the second snake on a "gameboard"
                label3.Text = "Yellow  0:0  Red";
                current = snake2.getHead();
                setHeadLabel(current.getBone(), images2);
                while (current.hasNext())
                {
                    current = current.getNext();
                    if (current.hasNext())
                        setBoneLabel(current.getBone(), images2);
                    else setTailLabel(current.getBone(), images2);
                }
            }
        }

        /// <summary>
        /// Called during grid-changing proces when user press object to put/remove "rock" from grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void putRock(object sender, System.EventArgs e)
        {
            if (changes)
            {
                if (((PictureBox)sender).Image == rock)
                    ((PictureBox)sender).Image = null;
                else if (((PictureBox)sender).Image == null)
                    ((PictureBox)sender).Image = rock;
            }
        }

        /// <summary>
        /// During changes-mode, changes color of a box that cursor is pointing to
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enterSpace(object sender, System.EventArgs e)
        {
            if (changes)
                ((PictureBox)sender).BackColor = System.Drawing.Color.LightGreen;
        }

        /// <summary>
        /// During changes-mode, changes color of a box back after cursor stops pointing to it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void leaveSpace(object sender, System.EventArgs e)
        {
            ((PictureBox)sender).BackColor = System.Drawing.Color.DarkGreen;
        }

        /// <summary>
        /// Method randonly choose spare coordinates for snake's food and put food picture there
        /// </summary>
        public void putFood()
        {
            int freespace = 0;
            for (int ii = 0; ii < x; ii++)
            {
                for (int jj = 0; jj < y; jj++)
                    if (picb[ii, jj].Image == null)
                        freespace++;
                if (freespace > 1)
                    break;
            }

            if (freespace == 1)
                win();
            else
            {
                Random ranI = new Random();
                Random ranJ = new Random();
                int i = ranI.Next(x) % x;
                int j = ranJ.Next(y) % y;
                int k = 0;
                while (picb[i, j].Image != null && k < 4)
                {
                    i = ranI.Next(x) % x;
                    j = ranJ.Next(y) % y;
                    k++;
                }

                int ii = 0; int jj = 0;

                if (k == 4)
                    for (i = 0; i < x; i++)
                    {
                        for (j = 0; j < y; j++)
                            if (picb[i, j].Image == null)
                            {
                                ii = i; jj = j;
                                break;
                            }
                        if (picb[ii, jj].Image == null)
                            break;
                    }
                try
                {
                    picb[i, j].Image = images[10];
                }
                catch (NullReferenceException e)
                {
                    picb[ii, jj].Image = images[10];
                }
            }
        }

        /// <summary>
        /// Opens main menu if this form is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            time.Stop();
            step.Stop();
            Form form = Application.OpenForms[0];
            form.Show();
        }
        #endregion

        #region on time

        /// <summary>
        /// Method that performs snake's movement after some set period of time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onTick(object sender, EventArgs e)
        {
            dir = checkDirection(dir, queue); // get new direction if any key was pressed
            makemove(snake, dir, images); // move

            if (iftwo)
            { // same for the second snake if exist
                dir2 = checkDirection(dir2, queue2);
                makemove(snake2, dir2, images2);
            }
        }

        /// <summary>
        /// Method iteract through the queue if any key was pressed and returns new direction for a snake
        /// </summary>
        /// <param name="dir">old direction</param>
        /// <param name="queue">queue of direction</param>
        /// <returns>int - direction where snake should move next</returns>
        private int checkDirection(int dir, Queue<int> queue)
        {
            while (queue.Count != 0)
            { // rules doesn't allow to turn snake 180 degrees at once, so direction that is allowed should be found
                int d = queue.Dequeue();
                if (dir == 2 && (d == 4 || d == 6))
                { return d; }
                else if (dir == 4 && (d == 2 || d == 8))
                { return d; }
                else if (dir == 6 && (d == 2 || d == 8))
                { return d; }
                else if (dir == 8 && (d == 4 || d == 6))
                { return d; }
            }
            return dir;
        }

        /// <summary>
        /// Method moves snake's head and tail if needed. it also put a new food if it was eaten
        /// </summary>
        /// <param name="snake">snake to be moves</param>
        /// <param name="dir">direction that snake should follow</param>
        /// <param name="im">array of images for the snake</param>
        private void makemove(Snake snake, int dir, Image[] im)
        {
            Coordinates c = snake.moveHead(dir);
            if (c.inRange())
            {//doesn't allow outofboundsexception
                if (picb[c.getX(), c.getY()].Image == null)
                {//if head ocupies empty cell, then move tail
                    setHeadLabel(snake.getHead().getBone(), im);
                    setBoneLabel(snake.getHead().getNext().getBone(), im);
                    c = snake.moveTail();
                    picb[c.getX(), c.getY()].Image = null;
                    if (snake.getHead() != snake.getTail())
                        setTailLabel(snake.getTail().getBone(), im);
                }
                else if (picb[c.getX(), c.getY()].Image == images[10])
                {// if head occupies cell with food, then tail should not be moved to inctease snake's length. put new food
                    setHeadLabel(snake.getHead().getBone(), im);
                    if (points == 0)
                        setTailLabel(snake.getTail().getBone(), im);
                    else
                        setBoneLabel(snake.getHead().getNext().getBone(), im);
                    points++;
                    if (iftwo)
                    {
                        label3.Text = "Yellow  " + (this.snake.getLength() - 1).ToString() + ":" + (this.snake2.getLength() - 1).ToString() + "  Red";
                    }
                    else
                    {
                        label3.Text = points.ToString();
                    }
                    if (sound != null)
                        sound.Play();
                    putFood();
                    switch (points)
                    {
                        case 5: step.Interval = 400; break;
                        case 10: step.Interval = 350; break;
                        case 20: step.Interval = 300; break;
                        case 40: step.Interval = 250; break;
                        case 66: step.Interval = 200; break;
                        case 88: step.Interval = 100; break;

                    }
                }
                // snake pumps into a rock
                else if ((picb[c.getX(), c.getY()].Image == rock))
                {
                    gameOver("OUPS! One silly snake but bumped into the rock! Do you want to try with another one? Otherwise you return to menu.");
                }
                // snake eats itself
                else
                {
                    gameOver("OUPS! One snake is not in mood today. It tries to bite itself or others. Do you want to try with another one? Otherwise you return to menu.");
                }
            }
            // snake bumps into the wall
            else
            {
                gameOver("OUPS! One cunning snake tryed to escape, but bumped into the wall! Do you want to try with another one? Otherwise you return to menu.");
            }
        }

        /// <summary>
        /// Method that isresponsible for changing time counter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerOn(object sender, EventArgs e)
        {
            sec++;
            if (sec > 59)
            {
                sec = sec - 60;
                min++;
            }
            String s = "";
            if (min < 10)
                s += "0";
            s += min.ToString() + ":";

            if (sec < 10)
                s += "0";
            s += sec.ToString();
            label4.Text = s;
        }

        #endregion

        #region move
        /// <summary>
        /// Method that set head's image in new coordinates
        /// </summary>
        /// <param name="bone">Bone that should be displayed by picture</param>
        /// <param name="images">array of images with the snake</param>
        private void setHeadLabel(Bone bone, Image[] images)
        {
            switch (bone.getDirection())
            {// according to the direction best image is set
                case 0: picb[bone.getCoord().getX(), bone.getCoord().getY()].Image = images[6]; break;
                case 2: picb[bone.getCoord().getX(), bone.getCoord().getY()].Image = images[2]; break;
                case 4: picb[bone.getCoord().getX(), bone.getCoord().getY()].Image = images[4]; break;
                case 6: picb[bone.getCoord().getX(), bone.getCoord().getY()].Image = images[6]; break;
                case 8: picb[bone.getCoord().getX(), bone.getCoord().getY()].Image = images[8]; break;
            }
        }

        /// <summary>
        /// Method that set bone's image in new coordinates
        /// </summary>
        /// <param name="bone">Bone that should be displayed by picture</param>
        /// <param name="images">array of images with the snake</param>
        private void setBoneLabel(Bone bone, Image[] images)
        {
            switch (bone.getDirection())
            {// according to the direction best image is set
                case 1: picb[bone.getCoord().getX(), bone.getCoord().getY()].Image = images[1]; break;
                case 2: picb[bone.getCoord().getX(), bone.getCoord().getY()].Image = images[5]; break;
                case 3: picb[bone.getCoord().getX(), bone.getCoord().getY()].Image = images[3]; break;
                case 4: picb[bone.getCoord().getX(), bone.getCoord().getY()].Image = images[0]; break;
                case 6: picb[bone.getCoord().getX(), bone.getCoord().getY()].Image = images[0]; break;
                case 7: picb[bone.getCoord().getX(), bone.getCoord().getY()].Image = images[7]; break;
                case 8: picb[bone.getCoord().getX(), bone.getCoord().getY()].Image = images[5]; break;
                case 9: picb[bone.getCoord().getX(), bone.getCoord().getY()].Image = images[9]; break;
            }
        }

        /// <summary>
        /// Method that set tail's image in new coordinates
        /// </summary>
        /// <param name="bone">Bone that should be displayed by picture</param>
        /// <param name="images">array of images with the snake</param>
        private void setTailLabel(Bone bone, Image[] images)
        {
            switch (bone.getDirection())
            { // according to the direction best image is set
                case 2: picb[bone.getCoord().getX(), bone.getCoord().getY()].Image = images[11]; break;
                case 4: picb[bone.getCoord().getX(), bone.getCoord().getY()].Image = images[12]; break;
                case 6: picb[bone.getCoord().getX(), bone.getCoord().getY()].Image = images[13]; break;
                case 8: picb[bone.getCoord().getX(), bone.getCoord().getY()].Image = images[14]; break;
            }
        }

        #endregion

        #region key settings
        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.A:
                case Keys.W:
                case Keys.S:
                case Keys.D:
                case Keys.P:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.KeyCode)
            {
                case Keys.Left: queue.Enqueue(4); break;
                case Keys.Right: queue.Enqueue(6); break;
                case Keys.Up: queue.Enqueue(8); break;
                case Keys.Down: queue.Enqueue(2); break;
                case Keys.P: gamePause(); break;
            }
            if (iftwo)
                switch (e.KeyCode)
                {
                    case Keys.A: queue2.Enqueue(4); break;
                    case Keys.D: queue2.Enqueue(6); break;
                    case Keys.W: queue2.Enqueue(8); break;
                    case Keys.S: queue2.Enqueue(2); break;
                }
        }

        #endregion

        /// <summary>
        /// Pauses the game and asks user to choose further action
        /// </summary>
        private void gamePause()
        {
            time.Stop();
            step.Stop();
            DialogResult res = MessageBox.Show("Do you want to continue? Otherwise you return to menu?", "Pause", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                time.Start();
                step.Start();
            }
            else
            {
                Form form = Application.OpenForms[0];
                form.Show();
                this.Close();
            }
        }

        /// <summary>
        /// Cleans the gameboard from snake's pieces and restarts the game
        /// </summary>
        private void restart()
        {
            time = new Timer();
            step = new Timer();
            snake = new Snake(0, 0);
            dir = 6;
            queue = new Queue<int>();
            points = 0;
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    if (picb[i, j].Image != rock)
                        picb[i, j].Image = null;
            picb[0, 0].Image = images[6];

            if (iftwo)
            {
                snake2 = new Snake(x - 1, y - 1);
                dir2 = 4;
                queue2 = new Queue<int>();
                picb[x - 1, y - 1].Image = images2[4];
                label3.Text = "Yellow  0:0  Red";
            }
            else label3.Text = "0";
            putFood();
            timeOn();
        }

        /// <summary>
        /// Method stops the game and shows message with explanation of the reason game ended
        /// </summary>
        /// <param name="s">String - message with explanation of the reason game ended</param>
        private void gameOver(String s)
        {
            step.Stop(); time.Stop();
            DialogResult res = MessageBox.Show("You survived: " + label4.Text + "\n"+ s, "Game Over", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                restart();
            }
            else
            {
                Form form = Application.OpenForms[0];
                form.Show();
                this.Close();
            }

        }

        /// <summary>
        /// Method stops the game and shows congratulation message
        /// </summary>
        private void win()
        {
            step.Stop(); time.Stop();
            DialogResult res = MessageBox.Show("Congratulations! You managed to train and feel you snake! It took you: " + label4.Text + " Do you want to play one more time? Otherwise you return to menu.", "Congratulations", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                restart();
            }
            else
            {
                Form form = Application.OpenForms[0];
                form.Show();
                this.Close();
            }
        }
    }
}
