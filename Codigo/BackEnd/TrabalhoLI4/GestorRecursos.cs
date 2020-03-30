using System;
using System.Collections;
using System.Collections.Generic;
using LI4;

public class GestorRecursos
{
    private InstituicaoDAO instituicoes;
    private VisitanteDAO visitantes;
    private ColaboradorDAO colaboradores;
    private AdministradorDAO admins;

    public GestorRecursos()
    {
        this.instituicoes = new InstituicaoDAO();
        this.visitantes = new VisitanteDAO();
        this.colaboradores = new ColaboradorDAO();
        this.admins = new AdministradorDAO();
    }
}
