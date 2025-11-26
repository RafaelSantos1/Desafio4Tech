import { Component, OnInit } from '@angular/core';
import { PlanoServico } from '../plano.servico';
import { Plano } from '../plano.model';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-plano-lista',
  standalone: true,
  imports: [RouterModule,CommonModule],
  templateUrl: './plano-lista.html',
  styleUrl: './plano-lista.css',
})
export class PlanoLista implements OnInit {
  planos:  Plano[] = [];

  constructor(private planoServico: PlanoServico, 
              private toastr: ToastrService) {}

  ngOnInit(): void {
    this.loadPlanos();
  }

  loadPlanos() {
    this.planoServico.getAll().subscribe(result => {
      this.planos = result.dados;
    });
  }

  deletePlano(id: number) {
    if(confirm('Deseja realmente excluir este plano?')){
      this.planoServico.delete(id).subscribe({
      next: (response) => {
        this.toastr.success(response.mensagem, 'Sucesso'); 
        this.loadPlanos()
      },
      error: (err) => {
          this.toastr.error(err.error.mensagem, 'Erro');
      }
  });

    }
  }
}
