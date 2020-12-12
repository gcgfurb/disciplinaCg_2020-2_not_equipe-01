﻿using System;
using CG_Biblioteca;
using gcgcg;
using OpenTK.Graphics.OpenGL;

namespace CG_N3
{
    internal class Bloco : Retangulo
    {
        public Bloco(char rotulo, Objeto paiRef, Ponto4D ptoInfEsq, Ponto4D ptoSupDir, BlocoType blocoType) : base(rotulo, paiRef, ptoInfEsq, ptoSupDir)
        {
            base.PontosAdicionar(ptoInfEsq);
            base.PontosAdicionar(new Ponto4D(ptoSupDir.X, ptoInfEsq.Y));
            base.PontosAdicionar(ptoSupDir);
            base.PontosAdicionar(new Ponto4D(ptoInfEsq.X, ptoSupDir.Y));
            BlocoType = blocoType;
            Mode = "Mode1";

            switch (BlocoType)
            {
                case BlocoType.T:
                    this.GenerateT();
                    break;
                case BlocoType.I:
                    this.GenerateI();
                    break;
                default:
                    break;
            }
        }

        public BlocoType BlocoType { get; }

        internal string Mode { get; set; }

        public void GenerateT()
        {
            Ponto4D pto = pontosLista[0];
            this.FilhoAdicionar(new Retangulo('L', this, new Ponto4D(pto.X - 30, pto.Y), new Ponto4D(pto.X, pto.Y + 30)));
            this.FilhoAdicionar(new Retangulo('R', this, new Ponto4D(pto.X + 30, pto.Y), new Ponto4D(pto.X + 60, pto.Y + 30)));
            this.FilhoAdicionar(new Retangulo('B', this, new Ponto4D(pto.X, pto.Y - 30), new Ponto4D(pto.X + 30, pto.Y)));
        }

        public void GenerateI()
        {
            Ponto4D pto = pontosLista[0];
            this.FilhoAdicionar(new Retangulo('A', this, new Ponto4D(pto.X, pto.Y + 30), new Ponto4D(pto.X + 30, pto.Y + 60)));
            this.FilhoAdicionar(new Retangulo('B', this, new Ponto4D(pto.X, pto.Y - 30), new Ponto4D(pto.X + 30, pto.Y)));
            this.FilhoAdicionar(new Retangulo('C', this, new Ponto4D(pto.X, pto.Y - 60), new Ponto4D(pto.X + 30, pto.Y - 30)));
        }

        public void Rotate()
        {
            switch (BlocoType)
            {
                case BlocoType.T:
                    this.RotateT();
                    break;
                case BlocoType.I:
                    this.RotateI();
                    break;
                default:
                    break;
            }
        }

        private void RotateT()
        {
            foreach (var filho in this.GetFilhos())
            {
                switch (filho.rotulo)
                {
                    case 'L':
                        foreach (var pto in filho.pontosLista)
                        {
                            pto.X += 30;
                            pto.Y -= 30;
                        }
                        filho.rotulo = 'B';
                        break;
                    case 'B':
                        foreach (var pto in filho.pontosLista)
                        {
                            pto.X += 30;
                            pto.Y += 30;
                        }
                        filho.rotulo = 'R';
                        break;
                    case 'R':
                        foreach (var pto in filho.pontosLista)
                        {
                            pto.X -= 30;
                            pto.Y += 30;
                        }
                        filho.rotulo = 'T';
                        break;
                    case 'T':
                        foreach (var pto in filho.pontosLista)
                        {
                            pto.X -= 30;
                            pto.Y -= 30;
                        }
                        filho.rotulo = 'L';
                        break;
                    default:
                        break;
                }
            }
        }

        private void RotateI()
        {
            switch (this.Mode)
            {
                case "Mode1":
                    foreach (var filho in this.GetFilhos())
                    {
                        switch (filho.rotulo)
                        {
                            case 'A':
                                foreach (var pto in filho.pontosLista)
                                {
                                    pto.X -= 30;
                                    pto.Y -= 30;
                                }
                                break;
                            case 'B':
                                foreach (var pto in filho.pontosLista)
                                {
                                    pto.X += 30;
                                    pto.Y += 30;
                                }
                                break;
                            case 'C':
                                foreach (var pto in filho.pontosLista)
                                {
                                    pto.X += 60;
                                    pto.Y += 60;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    this.Mode = "Mode2";
                    break;
                case "Mode2":
                    foreach (var filho in this.GetFilhos())
                    {
                        switch (filho.rotulo)
                        {
                            case 'A':
                                foreach (var pto in filho.pontosLista)
                                {
                                    pto.X += 30;
                                    pto.Y += 30;
                                }
                                break;
                            case 'B':
                                foreach (var pto in filho.pontosLista)
                                {
                                    pto.X -= 30;
                                    pto.Y -= 30;
                                }
                                break;
                            case 'C':
                                foreach (var pto in filho.pontosLista)
                                {
                                    pto.X -= 60;
                                    pto.Y -= 60;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    this.Mode = "Mode1";
                    break;
                default:
                    break;
            }
        }
    }
}
