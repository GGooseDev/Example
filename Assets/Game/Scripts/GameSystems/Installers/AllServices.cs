using System;
using UnityEngine;

public class AllServices
{
    private static AllServices _instance;
    public static AllServices Container => _instance ??= new AllServices();

    public void RegisterSingle<TService>(TService implement) where TService : IService
    {
        Implementation<TService>.ServiceInstance = implement;
    }
   
    public TService Single<TService>() where TService : IService
    {
        return Implementation<TService>.ServiceInstance;
    }

    
    private static class Implementation<T> where T : IService
    {
        public static T ServiceInstance;
    }
    
 
}