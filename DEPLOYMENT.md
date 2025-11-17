# Deployment Guide

This document explains how to deploy the EDH Deck Optimizer application to various hosting platforms.

## Recommended: Vercel (Free)

Vercel provides the easiest and most feature-rich deployment option with zero configuration required.

### Steps:

1. **Sign up for Vercel**: Go to [vercel.com](https://vercel.com) and sign up with your GitHub account
2. **Import Project**: Click "New Project" and import the `Moxfield.Deck.Optimizer` repository
3. **Configure**:
   - Framework Preset: Vite
   - Root Directory: `frontend`
   - Build Command: `npm run build`
   - Output Directory: `dist`
4. **Deploy**: Click "Deploy" and wait for the build to complete
5. **Done**: Your app will be live at `https://your-project.vercel.app`

### Benefits:
- ✅ Free hosting with generous limits
- ✅ Automatic HTTPS
- ✅ Global CDN
- ✅ Automatic deployments on git push
- ✅ Preview deployments for PRs
- ✅ Zero configuration required

### Cost: $0/month

## Alternative: Netlify (Free)

Netlify is another excellent option with similar features to Vercel.

### Steps:

1. **Sign up for Netlify**: Go to [netlify.com](https://netlify.com)
2. **Import from Git**: Connect your GitHub repository
3. **Configure**:
   - Base directory: `frontend`
   - Build command: `npm run build`
   - Publish directory: `frontend/dist`
4. **Deploy**: Click "Deploy site"

### Benefits:
- ✅ Free hosting
- ✅ Automatic HTTPS
- ✅ Global CDN
- ✅ Continuous deployment
- ✅ Form handling (if needed later)

### Cost: $0/month

## Alternative: GitHub Pages (Free)

GitHub Pages is a good option for simpler deployments but requires more manual setup.

### Steps:

1. **Create deployment workflow**: Add `.github/workflows/deploy.yml`:

```yaml
name: Deploy to GitHub Pages

on:
  push:
    branches: [main]
  workflow_dispatch:

permissions:
  contents: read
  pages: write
  id-token: write

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - uses: actions/setup-node@v4
        with:
          node-version: '20'
      - run: cd frontend && npm ci && npm run build
      - uses: actions/upload-pages-artifact@v2
        with:
          path: frontend/dist

  deploy:
    needs: build
    runs-on: ubuntu-latest
    environment:
      name: github-pages
      url: ${{ steps.deployment.outputs.page_url }}
    steps:
      - id: deployment
        uses: actions/deploy-pages@v2
```

2. **Enable GitHub Pages**: Go to repository Settings > Pages > Select "GitHub Actions" as source
3. **Push to main branch**: The workflow will deploy automatically

### Benefits:
- ✅ Free hosting
- ✅ Integrated with GitHub
- ✅ Simple for static sites

### Limitations:
- ❌ Manual workflow configuration
- ❌ Slower deployments
- ❌ No preview deployments

### Cost: $0/month

## Alternative: Cloudflare Pages (Free)

Cloudflare Pages offers excellent performance with their global CDN.

### Steps:

1. **Sign up**: Go to [pages.cloudflare.com](https://pages.cloudflare.com)
2. **Create a project**: Connect your GitHub repository
3. **Configure**:
   - Framework preset: None
   - Build command: `cd frontend && npm run build`
   - Build output directory: `frontend/dist`
4. **Deploy**: Save and deploy

### Benefits:
- ✅ Free hosting
- ✅ Excellent CDN performance
- ✅ Automatic HTTPS
- ✅ Git integration

### Cost: $0/month

## Comparison Table

| Feature | Vercel | Netlify | GitHub Pages | Cloudflare Pages |
|---------|--------|---------|--------------|------------------|
| Setup Ease | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐⭐ | ⭐⭐⭐ | ⭐⭐⭐⭐ |
| Build Speed | Fast | Fast | Medium | Fast |
| CDN | Global | Global | Limited | Global |
| Preview Deploys | ✅ | ✅ | ❌ | ✅ |
| Analytics | ✅ | ✅ | ❌ | ✅ |
| Custom Domain | ✅ Free | ✅ Free | ✅ Free | ✅ Free |
| Cost | $0 | $0 | $0 | $0 |

## Recommendation

**Use Vercel** for the best developer experience and features. It's our primary recommendation because:
- Zero configuration needed
- Excellent build performance
- Great preview deployments
- Built-in analytics
- Best integration with Vite/React

## Post-Deployment

After deploying to any platform:

1. **Test the application**: Visit your deployed URL and test all features
2. **Configure custom domain** (optional): Add your own domain in the platform settings
3. **Set up monitoring**: Enable analytics if available
4. **Update README**: Add the live URL to your repository README

## Environment Variables

Currently, the application doesn't require any environment variables. If you add backend functionality later, you can add environment variables in your hosting platform's dashboard.

## CI/CD

All recommended platforms (except GitHub Pages) automatically deploy when you push to the main branch. No additional CI/CD setup is required.

## Performance Optimization

The Vite build process automatically:
- Minifies JavaScript and CSS
- Optimizes images
- Code-splits for better loading
- Generates sourcemaps (in dev mode)

Your deployed site should score 90+ on Lighthouse for performance.

## Cost Breakdown

For a typical usage scenario:
- **Vercel Free Tier**: 100GB bandwidth, unlimited projects, 100 build minutes/day
- **Netlify Free Tier**: 100GB bandwidth, 300 build minutes/month
- **GitHub Pages**: 100GB bandwidth, 10 builds/hour
- **Cloudflare Pages**: Unlimited bandwidth, 500 builds/month

All platforms are sufficient for personal/small team use at **$0/month**.

## Support

If you encounter issues during deployment:
1. Check the platform's documentation
2. Review build logs in the platform dashboard
3. Ensure `frontend/package.json` has correct scripts
4. Verify Node.js version compatibility (v18+ recommended)
