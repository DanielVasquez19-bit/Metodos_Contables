using System;
using System.Drawing;
using System.Drawing.Drawing2D;

public class CaptchaGenerator
{
    public static string GenerarTextoCaptcha()
    {
        string caracteres = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghjkmnpqrstuvwxyz#$%&?¿23456789";
        Random rnd = new Random();
        char[] captcha = new char[6];

        for (int i = 0; i < captcha.Length; i++)
        {
            captcha[i] = caracteres[rnd.Next(caracteres.Length)];
        }

        return new string(captcha);
    }

    public static Bitmap GenerarImagenCaptcha(string texto)
    {
        int ancho = 200, alto = 50;
        Bitmap bmp = new Bitmap(ancho, alto);
        Graphics g = Graphics.FromImage(bmp);
        g.SmoothingMode = SmoothingMode.AntiAlias;

        g.FillRectangle(Brushes.White, 0, 0, ancho, alto);
        Random rnd = new Random();

        int espacioEntreCaracteres = ancho / (texto.Length + 2);
        int xPos = 10;

        foreach (char letra in texto)
        {
            int fontSize = 20;
            Font fuente = new Font("Arial", fontSize, FontStyle.Bold);

            int rotacion = rnd.Next(-10, 10);
            g.RotateTransform(rotacion);

            int yPos = alto / 4 + rnd.Next(-3, 3);

            g.DrawString(letra.ToString(), fuente, Brushes.Black, xPos, yPos);
            g.RotateTransform(-rotacion);

            xPos += espacioEntreCaracteres;
        }

        Pen pen = new Pen(Color.Gray, 2);
        for (int i = 0; i < 3; i++)
        {
            g.DrawLine(pen, rnd.Next(ancho), rnd.Next(alto), rnd.Next(ancho), rnd.Next(alto));
        }

        return bmp;
    }
}