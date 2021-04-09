import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthorizeGuard } from '../../api-authorization/authorize.guard';
import { BoardManagerComponent } from './board-manager/board-manager.component';
import { BoardComponent } from './board/board.component';


const routes: Routes =
  [
    {
      path: 'boards',
      canActivate: [AuthorizeGuard],
      component: BoardManagerComponent
    },
    {
      path: 'boards/:id',
      canActivate: [AuthorizeGuard],
      component: BoardComponent
    }
  ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BoardRoutingModule { }
