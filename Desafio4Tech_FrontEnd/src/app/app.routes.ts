
import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'planos',
    loadComponent: () => import('./componentes/plano/plano-lista/plano-lista')
                          .then(m => m.PlanoLista)
  },
  {
    path: 'planos/novo',
    loadComponent: () => import('./componentes/plano/plano-form/plano-form')
                          .then(m => m.PlanoForm)
  },
  {
    path: 'planos/editar/:id',
    loadComponent: () => import('./componentes/plano/plano-form/plano-form')
                          .then(m => m.PlanoForm)
  },
  {
    path: 'beneficiarios',
    loadComponent: () => import('./componentes/beneficiario/beneficiario-lista/beneficiario-lista')
                          .then(m => m.BeneficiarioLista)
  },
  {
    path: 'beneficiarios/novo',
    loadComponent: () => import('./componentes/beneficiario/beneficiario-form/beneficiario-form')
                          .then(m => m.BeneficiarioForm)
  },
  {
    path: 'beneficiarios/editar/:id',
    loadComponent: () => import('./componentes/beneficiario/beneficiario-form/beneficiario-form')
                          .then(m => m.BeneficiarioForm)
  },
  { path: '', redirectTo: '/planos', pathMatch: 'full' }
];
