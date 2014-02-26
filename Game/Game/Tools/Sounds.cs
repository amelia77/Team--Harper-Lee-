namespace Game.Tools
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Media;
    using System.Threading;

    public static class Sounds
    {
        public enum SoundEffects { Move, Shoot, EnemyIsDestroyed, BossIsDestroyed, RecieveBonus, GameOver }

        const string path = @"..\..\music\";

        public static Thread musicThread;
        static bool musicOn = true;
        static bool sfxOn = true;
        static bool musicAvalible = true;

        // A static constructor to load the game music when the method class is first used
        static Sounds() 
        {
            try
            {
                musicThread = new Thread(new ThreadStart(PlayMusic));
                if (musicOn)
                {
                    StartMusic();
                }
            }

            catch
            {

            }
        }

        internal static void PlayMusic()
        {
            SFX(SoundEffects.Move);
        }

        public static bool SfxON
        {
            get
            {
                return sfxOn;
            }

            set
            {
                if (value && !sfxOn)
                {
                    sfxOn = true;
                }

                if (!value && sfxOn)
                {
                    sfxOn = false;
                }
            }
        }

        public static bool MusicOn
        {
            get
            {
                return musicOn;
            }

            set
            {
                if (value && !musicOn && musicAvalible)
                {
                    StartMusic();
                    musicOn = true;
                }

                if (!value && musicOn && musicAvalible)
                {
                    StopMusic();
                    musicOn = false;
                }
            }
        }

        public static void SFX(SoundEffects sfx)
        {
            switch (sfx)
            {
                case SoundEffects.Move:
                    PlaySoundFromFile(@"..\..\music\music.wav");
                    break;
                case SoundEffects.GameOver:
                    PlaySoundFromFile(@"..\..\music\Death.wav");
                    break;
                case SoundEffects.Shoot:
                    PlaySoundFromFile(@"..\..\music\Shoot.wav");
                    break;
            } 
        }

        internal static void PlaySoundFromFile(string filePath)
        {
            using (SoundPlayer player = new SoundPlayer(filePath))
            {
                try
                {
                    player.Stop();
                    player.PlayLooping();

                }

                finally
                {
                    Sounds.SfxON = false;
                }
            }
        }

        internal static void StopMusic()
        {
            musicThread.Abort();
            musicThread = new Thread(new ThreadStart(PlayMusic));
        }

        internal static void StartMusic()
        {
            musicThread.Start();
        }
    }
}
