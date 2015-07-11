using Astro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AstroAssistant.Controls
{
    /// <summary>
    /// Suivez les étapes 1a ou 1b puis 2 pour utiliser ce contrôle personnalisé dans un fichier XAML.
    ///
    /// Étape 1a) Utilisation de ce contrôle personnalisé dans un fichier XAML qui existe dans le projet actif.
    /// Ajoutez cet attribut XmlNamespace à l'élément racine du fichier de balisage où il doit 
    /// être utilisé :
    ///
    ///     xmlns:MyNamespace="clr-namespace:AstroAssistant.Controls"
    ///
    ///
    /// Étape 1b) Utilisation de ce contrôle personnalisé dans un fichier XAML qui existe dans un autre projet.
    /// Ajoutez cet attribut XmlNamespace à l'élément racine du fichier de balisage où il doit 
    /// être utilisé :
    ///
    ///     xmlns:MyNamespace="clr-namespace:AstroAssistant.Controls;assembly=AstroAssistant.Controls"
    ///
    /// Vous devrez également ajouter une référence du projet contenant le fichier XAML
    /// à ce projet et régénérer pour éviter des erreurs de compilation :
    ///
    ///     Cliquez avec le bouton droit sur le projet cible dans l'Explorateur de solutions, puis sur
    ///     "Ajouter une référence"->"Projets"->[Recherchez et sélectionnez ce projet]
    ///
    ///
    /// Étape 2)
    /// Utilisez à présent votre contrôle dans le fichier XAML.
    ///
    ///     <MyNamespace:AstralChart/>
    ///
    /// </summary>
    [TemplatePart(Name = AstralChart.ChartSurfacePartName, Type = typeof(Canvas))]
    public class AstralChart : Control
    {
        internal const String ChartSurfacePartName = "PART_ChartSurface";

        Canvas _ChartSurface;
        
        Ellipse
            _Ellipse1 = new Ellipse(),
            _Ellipse2 = new Ellipse();
        Line[] _ZodiacSeparators = Enumerable.Range(0, 12).Select(i => new Line()).ToArray();

        static String ZodiacSymbolLetters = "♈♉♊♋♌♍♎♏♐♑♒♓";
        TextBlock[] _ZodiacSymbols = Enumerable.Range(0, 12).Select(i => new TextBlock() { Text = ZodiacSymbolLetters[i].ToString() }).ToArray();

        static AstralChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AstralChart), new FrameworkPropertyMetadata(typeof(AstralChart)));
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            InvalidateArrange();
        }

        /// <summary>
        /// Application du modèle
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _ChartSurface = (Canvas)GetTemplateChild(ChartSurfacePartName);
            _ChartSurface.Children.Clear();

            _ChartSurface.Children.Add(_Ellipse1);
            _ChartSurface.Children.Add(_Ellipse2);

            foreach (var line in _ZodiacSeparators)
                _ChartSurface.Children.Add(line);

            foreach (var zs in _ZodiacSymbols)
                _ChartSurface.Children.Add(zs);

            InitChartSurfaceElements();
        }

        void InitChartSurfaceElements()
        {
            double largeStrokeThickness = 1.0;

            _Ellipse1.Stroke = Brushes.Black;
            _Ellipse1.StrokeThickness = largeStrokeThickness;

            _Ellipse2.Stroke = Brushes.Black;
            _Ellipse2.StrokeThickness = largeStrokeThickness;

            foreach (var line in _ZodiacSeparators)
            {
                line.Stroke = Brushes.Black;
                line.StrokeThickness = largeStrokeThickness;
            }

            foreach (var zs in _ZodiacSymbols)
            {
                zs.Foreground = Brushes.DarkGray;
            }
        }

        static void RotatePoint(double centerX, double centerY, ref double pointX, ref double pointY, double angle)
        {
            double rAngle = angle * (Math.PI / 180);
            double cosTheta = Math.Cos(rAngle);
            double sinTheta = Math.Sin(rAngle);
            var x = (cosTheta * (pointX - centerX) - sinTheta * (pointY - centerY) + centerX);
            var y = (sinTheta * (pointX - centerX) + cosTheta * (pointY - centerY) + centerY);
            pointX = x;
            pointY = y;
        }
        void ArrangeChartSurface(double osize)
        {
            double margin = 2;
            double size = osize - (2 * margin);
            if (size <= 0) return;

            Canvas.SetTop(_Ellipse1, margin);
            Canvas.SetLeft(_Ellipse1, margin);
            _Ellipse1.Width = size;
            _Ellipse1.Height = size;

            double step = size * 0.15;
            Canvas.SetTop(_Ellipse2, margin + step);
            Canvas.SetLeft(_Ellipse2, margin + step);
            _Ellipse2.Width = Math.Max(size - (step * 2), 0);
            _Ellipse2.Height = Math.Max(size - (step * 2), 0);

            double angle = 0;
            if (NatalChart != null)
            {
                angle = NatalChart.Houses[0].Cusp;
            }
            for (int i = 0; i < 12; i++)
            {
                double angleSep = angle + (30 * i);
                var line = _ZodiacSeparators[i];
                Canvas.SetTop(line, margin);
                Canvas.SetLeft(line, margin);
                line.Width = size;
                line.Height = size;
                line.X1 = 0;
                line.Y1 = size / 2;
                line.X2 = step;
                line.Y2 = size / 2;
                line.RenderTransform = new RotateTransform(angleSep) {
                    CenterX = size / 2,
                    CenterY = size / 2
                };

                var zs = _ZodiacSymbols[i];
                double angleSymb = angleSep + 15;
                double px = margin + step / 2;
                double py = (osize / 2);
                RotatePoint(osize / 2, osize / 2, ref px, ref py, angleSymb);
                Canvas.SetTop(zs, py - (zs.ActualHeight / 2));
                Canvas.SetLeft(zs, px - (zs.ActualWidth / 2));
                //zs.RenderTransform = new RotateTransform(angleSymb) {
                //    CenterX = size / 2,
                //    CenterY = size / 2
                //};
            }
        }

        /// <summary>
        /// Calcul certaines tailles
        /// </summary>
        protected override Size MeasureOverride(Size constraint)
        {
            double size = Math.Min(constraint.Width, constraint.Height);
            if (size > 0)
            {
                for (int i = 0; i < 12; i++)
                {
                    var zs = _ZodiacSymbols[i];
                    zs.FontSize = (size * 0.15) / 3;
                }
            }
            return base.MeasureOverride(constraint);
        }

        /// <summary>
        /// Calcul la position de la surface de dessin
        /// </summary>
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            double size = Math.Min(arrangeBounds.Width, arrangeBounds.Height);
            var r = new Rect((arrangeBounds.Width - size) / 2, (arrangeBounds.Height - size) / 2, size, size);
            _ChartSurface.Arrange(r);
            ArrangeChartSurface(size);
            return arrangeBounds;
        }

        /// <summary>
        /// Thème astral
        /// </summary>
        public NatalChart NatalChart
        {
            get { return (NatalChart)GetValue(NatalChartProperty); }
            set { SetValue(NatalChartProperty, value); }
        }
        public static readonly DependencyProperty NatalChartProperty =
            DependencyProperty.Register("NatalChart", typeof(NatalChart), typeof(AstralChart), new PropertyMetadata(null, NatalChartProperty_Changed));
        private static void NatalChartProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AstralChart)d).InvalidateVisual();
        }

    }
}
