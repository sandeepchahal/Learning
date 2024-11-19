import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';
import { provideFirebaseApp, initializeApp } from '@angular/fire/app';
import { provideFirestore, getFirestore } from '@angular/fire/firestore';
import { environment } from './environments/environment';
import { AppComponent } from './app/app.component';
import { appRoutes } from './app/app.routes';

// Initialize Firebase app and ensure the environment configuration is correct
console.log(':testst');
console.log(environment.firebase);

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(appRoutes),
    provideHttpClient(),
    provideFirebaseApp(() => initializeApp(environment.firebase)), // Provide Firebase app
    provideFirestore(() => getFirestore()), // Provide Firestore
  ],
}).catch((err) => console.error('Bootstrap Error:', err));
