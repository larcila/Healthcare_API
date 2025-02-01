import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';  //is required to make calls to the Rest (also in app.config.ts => main.ts)

import { routes } from './app.routes';

@NgModule({ 
    declarations:[
    ],
    imports: [RouterModule.forRoot(routes),
       
    ],
    providers: [
        provideHttpClient()
    ],
    exports: [RouterModule]
})

export class AppRoutingModule {}

