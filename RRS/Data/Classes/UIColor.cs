class UIColor {
    public int R {get; private set;}
    public int G {get; private set;}
    public int B {get; private set;}

    public UIColor(int r=255, int g=255, int b=255) {
        //Check to see if the given inputs are within acceptable range
        bool valid_R_value = false;
        bool valid_G_value = false;
        bool valid_B_value = false;

        if (r >= 0 && r <= 255) {
            valid_R_value = true;
        }
        if (g >= 0 && g <= 255) {
            valid_G_value = true;
        }
        if (b >= 0 && b <= 255) {
            valid_B_value = true;
        }

        if (valid_R_value && valid_G_value && valid_B_value) {
            R = r;
            G = g;
            B = b;
        } else {
            string exceptionMessage = "";
            if (!valid_R_value) {
                exceptionMessage += "Invalid R value, value must be in the range of 0/255";
            }
            if (!valid_G_value) {
                exceptionMessage += "Invalid G value, value must be in the range of 0/255";
            }
            if (!valid_B_value) {
                exceptionMessage += "Invalid B value, value must be in the range of 0/255";
            }

            throw new ArgumentOutOfRangeException(exceptionMessage);
        }
    }

    public UIColor (string r, string g, string b) {
        bool rParsingSucces = false;
        bool gParsingSucces = false;
        bool bParsingSucces = false;

        int rParsed;
        int gParsed;
        int bParsed;

        if (int.TryParse(r, out rParsed))
        {
            rParsingSucces = true;
        }
        if (int.TryParse(g, out gParsed)) {
            gParsingSucces = true;
        }
        if (int.TryParse(b, out bParsed)) {
            bParsingSucces = true;
        }

        if (rParsingSucces && gParsingSucces && bParsingSucces) {
            //Check to see if the given inputs are within acceptable range
            bool valid_R_value = false;
            bool valid_G_value = false;
            bool valid_B_value = false;

            if (rParsed >= 0 && rParsed <= 255) {
                valid_R_value = true;
            }
            if (gParsed >= 0 && gParsed <= 255) {
                valid_G_value = true;
            }
            if (bParsed >= 0 && bParsed <= 255) {
                valid_B_value = true;
            }

            if (valid_R_value && valid_G_value && valid_B_value) {
                R = rParsed;
                G = gParsed;
                B = bParsed;
            } else {
                string exceptionMessage = "";
                if (!valid_R_value) {
                    exceptionMessage += "Invalid R value, value must be in the range of 0/255";
                }
                if (!valid_G_value) {
                    exceptionMessage += "Invalid G value, value must be in the range of 0/255";
                }
                if (!valid_B_value) {
                    exceptionMessage += "Invalid B value, value must be in the range of 0/255";
                }

                throw new ArgumentOutOfRangeException(exceptionMessage);
            }
        } else {
            string exceptionMessage = "";
            if (!rParsingSucces) {
                exceptionMessage += "Invalid R value string, value must be a number in the range of 0/255";
            }
            if (!gParsingSucces) {
                exceptionMessage += "Invalid G value string, value must be a number in the range of 0/255";
            }
            if (!bParsingSucces) {
                exceptionMessage += "Invalid B value string, value must be a number in the range of 0/255";
            }

            throw new ArgumentOutOfRangeException(exceptionMessage);
        }
    }
}