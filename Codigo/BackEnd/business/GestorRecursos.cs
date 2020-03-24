using System;
using System.Collections;
using Instituicao;
using Utilizador;

namespace LI4{
  private class GestorRecursos{

    private Dictionary<int, Instituicao> instituicoes;
    private Dictionary<int, Utilizador> utilizadores;

    public GestorRecursos()
    {
      this.instituicoes = new Dictionary<int, Instituicao>();
      this.utilizadores = new Dictionary<int, Utilizador>();
    }

    public GestorRecursos(GestorRecursos gr)
    {
      this.instituicoes = new Dictionary<int, Instituicao>(gr.getInstituicoes());
      this.utilizadores = new Dictionary<int, Utilizador>(gr.getUtilizadores());
    }

    public GestorRecursos(Dictionary<int, Instituicao> instituicoes, Dictionary<int, Utilizador> utilizadores)
    {
      this.instituicoes = new Dictionary<int, Instituicao>(instituicoes);
      this.utilizadores = new Dictionary<int, Utilizador>(utilizadores);
    }

    public Dictionary<int, Instituicao> getInstituicoes()
    {
      return this.instituicoes;
    }

    public Dictionary<int, Utilizador> getUtilizadores()
    {
      return this.utilizadores;
    }
  }
}
