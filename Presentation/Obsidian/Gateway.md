- [ ] Install Yarp.ReverseProxy
- [ ] Program.cs :
```
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
...
app.MapReverseProxy();
```

- [ ] appsettings.Development.json :
```
"ReverseProxy": {
  "Routes": {
    "catalog-route": {
      "ClusterId": "catalog-cluster",
      "Match": {
        "Path": "/catalog/{**remainder}"
      },
      "Transforms": [
        {
          "PathPattern": "/{**remainder}"
        }
      ]
    },
    "order-route": {
      "ClusterId": "order-cluster",
      "Match": {
        "Path": "/order/{**remainder}"
      },
      "Transforms": [
        {
          "PathPattern": "/{**remainder}"
        }
      ]
    }
  },

  "Clusters": {
    "catalog-cluster": {
      "Destinations": {
        "catalog-destination": {
          "Address": "https://localhost:7000/"
        }
      }
    },
    "order-cluster": {
      "Destinations": {
        "order-destination": {
          "Address": "https://localhost:7100/"
        }
      }
    }
  }
}
```