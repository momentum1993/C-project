using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Shisen_Sho {
    public class Tile {
        private Image image;

        public Tile(string imageName) {
            this.image = makeImage(imageName);
        }

        public Image getImage() {
            return image;
        }

        private Image makeImage(string imageName) {
            return Bitmap.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Shisen_Sho.tiles." + imageName + ".png"));
        }
    }
}
