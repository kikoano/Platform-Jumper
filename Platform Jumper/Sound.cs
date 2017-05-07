using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Platform_Jumper
{
    public class Sound
    {
        private string path;
        private SoundPlayer soundPlayer = new SoundPlayer();

        public static Sound Collect = new Sound(@"\Sounds\collect.wav");
        public static Sound Jump = new Sound(@"\Sounds\jump.wav");
        public static Sound Hit = new Sound(@"\Sounds\hit.wav");
        public static Sound Death = new Sound(@"\Sounds\death.wav");
        public static Sound Click = new Sound(@"\Sounds\click.wav");
        public static Sound Select = new Sound(@"\Sounds\select.wav");

        public Sound(string path)
        {
            this.path = path;
            soundPlayer = new SoundPlayer(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + path);
        }
        public void Play()
        {
            soundPlayer.Play();
        }
        public void PlayLoop()
        {
            soundPlayer.PlayLooping();
        }
    }
}
