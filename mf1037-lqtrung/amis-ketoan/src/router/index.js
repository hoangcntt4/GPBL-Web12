import Vue from 'vue'
import VueRouter from 'vue-router'
/* import Home from '../views/Home.vue' */
import TheContent from '../components/layout/TheContent.vue'
import Employee from '../views/employees/Employee.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Employee',
    component: Employee
  },
  {
    path: '/about',
    name: 'About',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  },
  {
    path: '/content',
    name: 'TheContent',
    component: TheContent
  },
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
