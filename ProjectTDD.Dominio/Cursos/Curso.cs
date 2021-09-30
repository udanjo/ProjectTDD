﻿using System;

namespace ProjectTDD.Dominio.Cursos
{
    public class Curso
    {
        public Curso(string nome, double cargaHoraria, PublicoAlvo publicoAlvo, double valor, string descricao)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException("Nome de campo obrigatório");

            if (cargaHoraria < 1)
                throw new ArgumentException("Carga Horária deveria ser maior que 0");

            if (valor < 1)
                throw new ArgumentException("Valor deveria ser maior que 0");

            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
            Descricao = descricao;
        }

        public string Nome { get; private set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public double Valor { get; private set; }
        public string Descricao { get; private set; }
    }
}