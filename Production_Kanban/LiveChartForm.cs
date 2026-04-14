using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MaterialSkin.Controls;


namespace Production_Kanban
{
    public partial class LiveChartForm : MaterialForm
    {
        public LiveChartForm()
        {
            InitializeComponent();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        private void LiveChartForm_Load(object sender, EventArgs e)
        {
            cartesianChart4.Series = new SeriesCollection
            {
            //    new LineSeries
            //    {
            //        Values = new ChartValues<ObservablePoint>
            //        {
            //            new ObservablePoint(0,10),      //First Point of First Line
            //            new ObservablePoint(4,7),       //2nd POint
            //            new ObservablePoint(5,3),     //------
            //            new ObservablePoint(7,6),
            //            new ObservablePoint(10,8)
            //        },
            //        PointGeometrySize = 25
            //    },
            //    new LineSeries
            //    {
            //        Values = new ChartValues<ObservablePoint>
            //        {
            //            new ObservablePoint(0,2),      //First Point of 2nd Line
            //            new ObservablePoint(2,5),       //2nd POint
            //            new ObservablePoint(3,6),     //------
            //            new ObservablePoint(6,8),
            //            new ObservablePoint(10,5)
            //        },
            //        PointGeometrySize = 15
            //    },
            //new LineSeries
            //    {
            //        Values = new ChartValues<ObservablePoint>
            //        {
            //            new ObservablePoint(0,4),      //First Point of 3rd Line
            //            new ObservablePoint(5,5),       //2nd POint
            //            new ObservablePoint(7,7),     //------
            //            new ObservablePoint(9,10),
            //            new ObservablePoint(10,9)
            //        },
            //        PointGeometrySize = 15
            //    }
             new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<double> {4, 6, 5, 2, 7}
                },
                new LineSeries
                {
                    Title = "Series 2",
                    Values = new ChartValues<double> {6, 7, 3, 4, 6},
                    PointGeometry = null
                },
                new LineSeries
                {
                    Title = "Series 2",
                    Values = new ChartValues<double> {5, 2, 8, 3},
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15
                }
            };

            cartesianChart4.AxisX.Add(new Axis
            {
                Title = "Month",
                Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" }
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Sales",
                LabelFormatter = value => value.ToString("C")
            });

            //vertical line series
            cartesianChart1.Series = new SeriesCollection
            {
                new VerticalLineSeries
                {
                    Values=new ChartValues<ObservableValue>
                    {
                            new ObservableValue(4),
                            new ObservableValue(2),
                            new ObservableValue(8),
                            new ObservableValue(2),
                            new ObservableValue(3),
                            new ObservableValue(0),
                            new ObservableValue(1),
                    },
                    DataLabels = true,
                    LabelPoint = point => point.Y + "K"
                }
            };

            cartesianChart2.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<ObservableValue>
                        {
                            new ObservableValue(4),
                            new ObservableValue(2),
                            new ObservableValue(8),
                            new ObservableValue(2),
                            new ObservableValue(3),
                            new ObservableValue(0),
                            new ObservableValue(1),
                        },
                    DataLabels = true,
                    LabelPoint = point => point.Y + "K"
                }
            };

            //Row Series
            cartesianChart3.Series = new SeriesCollection
            {
                new RowSeries
                {

                    Values = new ChartValues<ObservableValue>
                        {
                            new ObservableValue(1),
                            new ObservableValue(2),
                            new ObservableValue(3),
                            new ObservableValue(4),
                            new ObservableValue(5),
                            new ObservableValue(6),
                            new ObservableValue(7),
                        },
                    DataLabels = true,
                    LabelPoint = point => point.Y + "K"
                }
            };

            //Stacked Area Series
            cartesianChart5.Series = new SeriesCollection
            {
                new StackedAreaSeries
                {
                    Title = "Africa",
                    Values = new ChartValues<DateTimePoint>
                    {
                        new DateTimePoint(new System.DateTime(1950, 1, 1), .228),
                        new DateTimePoint(new System.DateTime(1960, 1, 1), .285),
                        new DateTimePoint(new System.DateTime(1970, 1, 1), .366),
                        new DateTimePoint(new System.DateTime(1980, 1, 1), .478),
                        new DateTimePoint(new System.DateTime(1990, 1, 1), .629),
                        new DateTimePoint(new System.DateTime(2000, 1, 1), .808),
                        new DateTimePoint(new System.DateTime(2010, 1, 1), 1.031),
                        new DateTimePoint(new System.DateTime(2013, 1, 1), 1.110)
                    },
                    LineSmoothness = 0
                },
                new StackedAreaSeries
                {
                    Title = "N & S America",
                    Values = new ChartValues<DateTimePoint>
                    {
                        new DateTimePoint(new System.DateTime(1950, 1, 1), .339),
                        new DateTimePoint(new System.DateTime(1960, 1, 1), .424),
                        new DateTimePoint(new System.DateTime(1970, 1, 1), .519),
                        new DateTimePoint(new System.DateTime(1980, 1, 1), .618),
                        new DateTimePoint(new System.DateTime(1990, 1, 1), .727),
                        new DateTimePoint(new System.DateTime(2000, 1, 1), .841),
                        new DateTimePoint(new System.DateTime(2010, 1, 1), .942),
                        new DateTimePoint(new System.DateTime(2013, 1, 1), .972)
                    },
                    LineSmoothness = 0
                },
                new StackedAreaSeries
                {
                    Title = "Asia",
                    Values = new ChartValues<DateTimePoint>
                    {
                        new DateTimePoint(new System.DateTime(1950, 1, 1), 1.395),
                        new DateTimePoint(new System.DateTime(1960, 1, 1), 1.694),
                        new DateTimePoint(new System.DateTime(1970, 1, 1), 2.128),
                        new DateTimePoint(new System.DateTime(1980, 1, 1), 2.634),
                        new DateTimePoint(new System.DateTime(1990, 1, 1), 3.213),
                        new DateTimePoint(new System.DateTime(2000, 1, 1), 3.717),
                        new DateTimePoint(new System.DateTime(2010, 1, 1), 4.165),
                        new DateTimePoint(new System.DateTime(2013, 1, 1), 4.298)
                    },
                    LineSmoothness = 0
                },
                new StackedAreaSeries
                {
                    Title = "Europe",
                    Values = new ChartValues<DateTimePoint>
                    {
                        new DateTimePoint(new System.DateTime(1950, 1, 1), .549),
                        new DateTimePoint(new System.DateTime(1960, 1, 1), .605),
                        new DateTimePoint(new System.DateTime(1970, 1, 1), .657),
                        new DateTimePoint(new System.DateTime(1980, 1, 1), .694),
                        new DateTimePoint(new System.DateTime(1990, 1, 1), .723),
                        new DateTimePoint(new System.DateTime(2000, 1, 1), .729),
                        new DateTimePoint(new System.DateTime(2010, 1, 1), .740),
                        new DateTimePoint(new System.DateTime(2013, 1, 1), .742)
                    },
                    LineSmoothness = 0
                }
            };
            cartesianChart5.AxisX.Add(new Axis
            {
                LabelFormatter = val => new System.DateTime((long)val).ToString("yyyy")
            });
            cartesianChart5.AxisY.Add(new Axis
            {
                LabelFormatter = val => val.ToString("N") + " M"
            });



            //Vertical StackedArea Series
            cartesianChart6.Series = new SeriesCollection
            {
                new VerticalStackedAreaSeries
                {

                    Values = new ChartValues<ObservableValue>
                        {
                            new ObservableValue(1),
                            new ObservableValue(2),
                            new ObservableValue(3),
                            new ObservableValue(4),
                            new ObservableValue(5),
                            new ObservableValue(6),
                            new ObservableValue(7),
                        },
                    DataLabels = true,
                    LabelPoint = point => point.Y + "K"
                }
            };

            //Stacked Column Series
            cartesianChart7.Series = new SeriesCollection
            {
                new StackedColumnSeries
                {

                    Values = new ChartValues<ObservableValue>
                        {
                            new ObservableValue(1),
                            new ObservableValue(2),
                            new ObservableValue(3),
                            new ObservableValue(4),
                            new ObservableValue(5),
                            new ObservableValue(6),
                            new ObservableValue(7),
                        },
                    DataLabels = true,
                    LabelPoint = point => point.Y + "K"
                }
            };


            //Stacked Row Series
            cartesianChart8.Series = new SeriesCollection
            {
                new StackedRowSeries
                {

                    Values = new ChartValues<ObservableValue>
                        {
                            new ObservableValue(1),
                            new ObservableValue(2),
                            new ObservableValue(3),
                            new ObservableValue(4),
                            new ObservableValue(5),
                            new ObservableValue(6),
                            new ObservableValue(7),
                        },
                    DataLabels = true,
                    LabelPoint = point => point.Y + "K"
                }
            };


            //Heat Series
            var r = new Random();
            cartesianChart9.Series = new SeriesCollection
            {
                new HeatSeries
                {

                   Values = new ChartValues<HeatPoint>
                    {
                        //X means sales man
                        //Y is the day
                        //"Jeremy Swanson"
                        new HeatPoint(0, 0, r.Next(0, 10)),
                        new HeatPoint(0, 1, r.Next(0, 10)),
                        new HeatPoint(0, 2, r.Next(0, 10)),
                        new HeatPoint(0, 3, r.Next(0, 10)),
                        new HeatPoint(0, 4, r.Next(0, 10)),
                        new HeatPoint(0, 5, r.Next(0, 10)),
                        new HeatPoint(0, 6, r.Next(0, 10)),
                        //"Lorena Hoffman"
                        new HeatPoint(1, 0, r.Next(0, 10)),
                        new HeatPoint(1, 1, r.Next(0, 10)),
                        new HeatPoint(1, 2, r.Next(0, 10)),
                        new HeatPoint(1, 3, r.Next(0, 10)),
                        new HeatPoint(1, 4, r.Next(0, 10)),
                        new HeatPoint(1, 5, r.Next(0, 10)),
                        new HeatPoint(1, 6, r.Next(0, 10)),
                        //"Robyn Williamson"
                        new HeatPoint(2, 0, r.Next(0, 10)),
                        new HeatPoint(2, 1, r.Next(0, 10)),
                        new HeatPoint(2, 2, r.Next(0, 10)),
                        new HeatPoint(2, 3, r.Next(0, 10)),
                        new HeatPoint(2, 4, r.Next(0, 10)),
                        new HeatPoint(2, 5, r.Next(0, 10)),
                        new HeatPoint(2, 6, r.Next(0, 10)),
                        //"Carole Haynes"
                        new HeatPoint(3, 0, r.Next(0, 10)),
                        new HeatPoint(3, 1, r.Next(0, 10)),
                        new HeatPoint(3, 2, r.Next(0, 10)),
                        new HeatPoint(3, 3, r.Next(0, 10)),
                        new HeatPoint(3, 4, r.Next(0, 10)),
                        new HeatPoint(3, 5, r.Next(0, 10)),
                        new HeatPoint(3, 6, r.Next(0, 10)),
                        //"Essie Nelson"
                        new HeatPoint(4, 0, r.Next(0, 10)),
                        new HeatPoint(4, 1, r.Next(0, 10)),
                        new HeatPoint(4, 2, r.Next(0, 10)),
                        new HeatPoint(4, 3, r.Next(0, 10)),
                        new HeatPoint(4, 4, r.Next(0, 10)),
                        new HeatPoint(4, 5, r.Next(0, 10)),
                        new HeatPoint(4, 6, r.Next(0, 10))
                    },
                   DataLabels = true,
                }
            };

            cartesianChart9.AxisX.Add(new Axis
            {
                LabelsRotation = -15,
                Labels = new[]
                {
                    "Jeremy Swanson",
                    "Lorena Hoffman",
                    "Robyn Williamson",
                    "Carole Haynes",
                    "Essie Nelson"
                },
                Separator = new Separator { Step = 1 }
            });
            cartesianChart9.AxisY.Add(new Axis
            {
                Labels = new[]
                {
                    "Monday",
                    "Tuesday",
                    "Wednesday",
                    "Thursday",
                    "Friday",
                    "Saturday",
                    "Sunday"
                }
            });

            //OHCL series(Financial)
            cartesianChart10.Series = new SeriesCollection
            {
                new OhlcSeries
                {
                    Values = new ChartValues<OhlcPoint>
                    {
                        new OhlcPoint(32, 35, 30, 32),
                        new OhlcPoint(33, 38, 31, 37),
                        new OhlcPoint(35, 42, 30, 40),
                        new OhlcPoint(37, 40, 35, 38),
                        new OhlcPoint(35, 38, 32, 33)
                    }
                }
            };


            //Bubbles Series
            cartesianChart11.Series = new SeriesCollection
            {
                new ScatterSeries
                {
                    Values = new ChartValues<ScatterPoint>
                        {
                            //X  Y   W
                            new ScatterPoint(5, 5, 20),
                            new ScatterPoint(3, 4, 80),
                            new ScatterPoint(7, 2, 20),
                            new ScatterPoint(2, 6, 60),
                            new ScatterPoint(8, 2, 70)
                        },
                    MinPointShapeDiameter = 15,
                    MaxPointShapeDiameter = 45
                },
                    new ScatterSeries
                    {
                        Values = new ChartValues<ScatterPoint>
                        {
                            new ScatterPoint(7, 5, 1),
                            new ScatterPoint(2, 2, 1),
                            new ScatterPoint(1, 1, 1),
                            new ScatterPoint(6, 3, 1),
                            new ScatterPoint(8, 8, 1)
                        },
                        PointGeometry = DefaultGeometries.Triangle,
                        MinPointShapeDiameter = 15,
                        MaxPointShapeDiameter = 45
                    }
            };

            //StepLine 
            cartesianChart12.Series = new SeriesCollection
            {
                new StepLineSeries
                {
                   Values = new ChartValues<double> { 9, 6, 5, 7, 8, 9, 7, 6, 7, 5 }
                }
            };

            cartesianChart12.Series.Add(new StepLineSeries
            {
                Values = new ChartValues<double> { 1, 4, 3, 1, 4, 2, 1, 2, 3, 5 },
                StrokeThickness = 3,
                PointGeometry = null
            });

            //Func<ChartPoint, string> labelPoint = chartPoint =>
            //string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            //pieChart1.Series = new SeriesCollection
            //{
            //    new PieSeries
            //    {
            //        Title = "Maria",
            //        Values = new ChartValues<double> {3},
            //        PushOut = 15,
            //        DataLabels = true,
            //        LabelPoint = labelPoint
            //    },
            //    new PieSeries
            //    {
            //        Title = "Charles",
            //        Values = new ChartValues<double> {4},
            //        DataLabels = true,
            //        LabelPoint = labelPoint
            //    },
            //    new PieSeries
            //    {
            //        Title = "Frida",
            //        Values = new ChartValues<double> {6},
            //        DataLabels = true,
            //        LabelPoint = labelPoint
            //    },
            //    new PieSeries
            //    {
            //        Title = "Frederic",
            //        Values = new ChartValues<double> {2},
            //        DataLabels = true,
            //        LabelPoint = labelPoint
            //    }
            //};
            //pieChart1.LegendLocation = LegendLocation.Bottom;




        }
    }
}
