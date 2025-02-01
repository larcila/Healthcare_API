import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const isLoggedIn = this.authService.isLoggedIn();
    const userRole = this.authService.getUserRole(); // Obtener el rol del usuario

    if (!isLoggedIn) {
      this.router.navigate(['/login']);
      return false;
    }

    // Verificar si el usuario tiene el rol necesario
    const requiredRole = route.data['role'];
    if (requiredRole && userRole !== requiredRole && userRole !== 'ADMIN') {
      this.router.navigate(['/home']);
      return false;
    }

    return true;
  }
}
